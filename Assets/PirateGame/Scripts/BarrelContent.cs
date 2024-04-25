using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.UI;

public class BarrelContent : MonoBehaviour
{
    #region eventBus
    private void OnEnable()
    {
        GameEventBus.Subscribe(GameEventsType.INTERACT, AccessBarrel);
    }
    private void OnDisable()
    {
        GameEventBus.Unsubscribe(GameEventsType.INTERACT, AccessBarrel);
    }
    #endregion

    //[SerializeField] private BarrelInventory[] UISlots;
    public Transform SlotsParent;
    private List<BarrelSlot> barrelData;

    private InventoryManager inv;
    private playerInteraction playerInt;
    private playerController playerCtrl;
    private UIManager ui;

    private bool playerUsingBarrel = false;

    [Serializable]
    public struct inputValues
    {
        public int quantity;
        public ItemType type;
    }

    public inputValues[] startValues;

    private void Awake()
    {
        // Initialize BarrelData List
        //UISlots = new BarrelInventory[16];
        barrelData = new List<BarrelSlot>();
        
        inv = FindObjectOfType<InventoryManager>();
        playerInt = FindObjectOfType<playerInteraction>();
        playerCtrl = FindObjectOfType<playerController>();
        ui = FindObjectOfType<UIManager>();
    }

    private void Start()
    {
        foreach(inputValues value in startValues)
        {
            for (int i = 0; i < value.quantity; i++)
            {
                AddItemToBarrel(value.type);
            }
        }

    }

    #region Debug Buttons
    private void OnGUI()
    {
        if (GUILayout.Button("PrintList"))
        {
            printList();
        }
        else if (GUILayout.Button("AddItem 1"))
        {
            addItem1();
        }
        else if (GUILayout.Button("AddItem 2"))
        {
            addItem2();
        }
        else if (GUILayout.Button("AddItem 3"))
        {
            addItem3();
        }
        else if (GUILayout.Button("RemoveItem 1"))
        {
            removeItemFromBarrel(ItemType.Banana);
        }
        else if (GUILayout.Button("RemoveItem 2"))
        {
            removeItemFromBarrel(ItemType.CannonBall);
        }
        else if (GUILayout.Button("RemoveItem 2"))
        {
            removeItemFromBarrel(ItemType.Plank);
        }

    }

    private void printList()
    {
        if(barrelData.Count == 0) { Debug.LogWarning("The Barrel is Empty"); return; }
        foreach(BarrelSlot slot in barrelData)
        {
            Debug.Log(slot.id + "// " + slot.type + ": " + slot.quantity);
        }
    }

    private void addItem1()
    {
        AddItemToBarrel(ItemType.Banana);
    }

    private void addItem2()
    {
        AddItemToBarrel(ItemType.CannonBall);
    }

    private void addItem3()
    {
        AddItemToBarrel(ItemType.Plank);
    }
    #endregion
    
    private void AccessBarrel()
    {
        if (playerUsingBarrel == true)
        {
            // toggle Player Snapped
            playerUsingBarrel = false;

            // Get the Camera to be Snapped to the Player
            ui.closeContainerInventory();
            CloseBarrel();

            // Get the player Movement to lock
            playerCtrl.canMove = true;
            playerCtrl.updateMouse = true;

            //Debug.Log("Player is no longer snapped to the cannon");
        }

        else if (playerInt.checkObject() == null) { return; }
        else if (playerInt.checkObject().tag != "Interactable") { return; }
        else if (playerInt.checkObject().transform.parent.gameObject == this.gameObject)
        {
            //Debug.Log("Shoot Called");
            if (!playerUsingBarrel)
            {
                // togglePlayerSnapped
                playerUsingBarrel = true;

                // Get the player to stand at the playerPos;
                ui.openContainerInventory();
                OpenBarrel();

                // Get the player Movement to lock
                playerCtrl.canMove = false;
                playerCtrl.updateMouse = false;

            }
        }
    }

    public void AddItemToBarrel(ItemType type)
    {
        // Check if the Object already Exists
        for (int i = 0; i < barrelData.Count; i++)
        {
            if (barrelData[i].type == type)
            {
                BarrelSlot tmpBarrel = barrelData[i];
                tmpBarrel.quantity++;
                barrelData[i] = tmpBarrel;

                barrelData[i].addQuantity(1);
                //Debug.Log("The Barrel already contains " + type + " so increasing the quantity to " + barrelData[i].quantity);
                //RefreshDisplay();
                return;
            }
        }

        // If Object Doesn't Exist then Add a new Object to the List;

        //Check if its the first Object to be added to the Barrel;
        if(barrelData.Count == 0)
        {
            barrelData.Add(new BarrelSlot(0, 1, type));
        }
        else
        {
            barrelData.Add(new BarrelSlot(barrelData.Count, 1, type));
        }
        //RefreshDisplay();

        //Debug.Log("A new Barrel Slot has been added for: " + type + "with a quantity of: " + barrelData[barrelData.Count - 1].quantity);
    }

    public void removeItemFromBarrel(ItemType type) 
    {
        // Check if the object being removed quantity is more than 0
        for (int i = 0; i < barrelData.Count; i++)
        {
            if (barrelData[i].type == type)
            {
                // Check if the barrel slot holds more than one item
                if (barrelData[i].quantity > 1)
                {
                    // Extract the barrelData and change the quantity to later return it
                    BarrelSlot tmpBarrel = barrelData[i];
                    int prevQuantity = tmpBarrel.quantity;
                    tmpBarrel.quantity--;
                    barrelData[i] = tmpBarrel;

                    barrelData[i].removeQuantity(1);
                    //Debug.Log("The Barrel has " + prevQuantity + " of " + type + "; The new total is " + barrelData[i].quantity);
                    RefreshDisplay();
                    return;
                }
                // Else remove the object from the List
                else
                {
                    // Extract the barrelData and change the quantity to 0
                    BarrelSlot tmpBarrel = barrelData[i];
                    tmpBarrel.quantity = 0;
                    barrelData[i] = tmpBarrel;

                    barrelData.RemoveAt(i);
                    //Debug.Log("The Barrel no longer holds " + type);
                    //barrelData.Sort((x, y) => y.quantity.CompareTo(x.quantity));
                    RefreshDisplay();
                }
            }
        }

    }

    public void OpenBarrel()
    {
        RefreshDisplay();
    }

    public void CloseBarrel()
    {
        for (int i = 0; i < inv.UISlots.Length; i++)
        {
            inv.UISlots[i].exists.SetActive(false);

            inv.UISlots[i].itemIcon.GetComponent<Image>().sprite = null;
            inv.UISlots[i].itemIcon.SetActive(false);

            inv.UISlots[i].numberIcon.GetComponentInChildren<TextMeshProUGUI>().text = 0.ToString();
            inv.UISlots[i].numberIcon.SetActive(false);
        }
    }

    private void RefreshDisplay()
    {
        // Loop length of the BarrelSlots
        for (int i = 0; i < barrelData.Count; i++)
        {
            if (barrelData[i].type != ItemType.None)
            {
                //print("1 Executed: " + i);
                //print(inv.UISlots[i].exists);
                inv.UISlots[i].exists.SetActive(true);

                inv.UISlots[i].itemIcon.SetActive(true);
                inv.UISlots[i].itemIcon.GetComponent<Image>().sprite = inv.getTexture(barrelData[i].type);

                inv.UISlots[i].numberIcon.SetActive(true);
                inv.UISlots[i].numberIcon.GetComponentInChildren<TextMeshProUGUI>().text = barrelData[i].quantity.ToString();
            }
        }

        // Loop the Rest of UI Slots
        for (int i = barrelData.Count; i < inv.UISlots.Length; i++)
        {
            
            //print(inv.UISlots[i]);
            inv.UISlots[i].exists.SetActive(false);

            inv.UISlots[i].itemIcon.GetComponent<Image>().sprite = null;
            inv.UISlots[i].itemIcon.SetActive(false);

            inv.UISlots[i].numberIcon.GetComponentInChildren<TextMeshProUGUI>().text = 0.ToString();
            inv.UISlots[i].numberIcon.SetActive(false);
        }
    }

}

public struct BarrelInventory
{
    int id;

    public GameObject slot;
    public GameObject itemIcon;
    public GameObject numberIcon;
    public GameObject exists;
    public GameObject selected;

    public BarrelInventory(int id, GameObject slot, GameObject itemIcon, GameObject numberIcon, GameObject exists, GameObject selected)
    {
        this.id = id;
        this.slot = slot;
        this.itemIcon = itemIcon;
        this.numberIcon = numberIcon;
        this.exists = exists;
        this.selected = selected;
    }
}

public struct BarrelSlot
{
    public int id;
    public int quantity;
    public ItemType type;

    public BarrelSlot(int id, int quantity, ItemType type)
    {
        this.id = id;
        this.quantity = quantity;
        this.type = type;
    }

    public void addQuantity(int quantityAdded)
    {
        quantity += quantityAdded;
    }

    public void removeQuantity(int quantityRemoved)
    {
        quantity -= quantityRemoved;
    }
}