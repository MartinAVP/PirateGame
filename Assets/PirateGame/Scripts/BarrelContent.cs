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

    [SerializeField] private BarrelInventory[] UISlots;
    [SerializeField] private Transform SlotsParent;
    private List<BarrelSlot> barrelData;

    private InventoryManager inventoryManager;
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
        UISlots = new BarrelInventory[16];
        barrelData = new List<BarrelSlot>();
        
        inventoryManager = FindObjectOfType<InventoryManager>();
        playerInt = FindObjectOfType<playerInteraction>();
        playerCtrl = FindObjectOfType<playerController>();
        ui = FindObjectOfType<UIManager>();

        #region Init BarrelSlots

        int j = 0;
        foreach (Transform slot in SlotsParent)
        {
            // Creates a new Inventory Slot item;
            UISlots[j] = new BarrelInventory(j, slot.gameObject, null, null, null, null);
            UISlots[j].slot.name = "Container Slot " + (j + 1);

            //Define each child;
            foreach (Transform childItem in slot)
            {
                if (childItem.name == "ItemIcon")
                {
                    childItem.name = "ItemIcon";
                    UISlots[j].itemIcon = childItem.gameObject;
                }
                else if (childItem.name == "NumberSlot")
                {
                    childItem.name = "NumberBackground";
                    UISlots[j].numberIcon = childItem.gameObject;
                }
                else if (childItem.name == "Exists")
                {
                    childItem.name = "ItemExistsOutline";
                    UISlots[j].exists = childItem.gameObject;
                }
                else if (childItem.name == "Selected")
                {
                    childItem.name = "ItemSelectedOutline";
                    UISlots[j].selected = childItem.gameObject;
                }
            }

            j++;
        }
        #endregion
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

    private void AddItemToBarrel(ItemType type)
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
                RefreshDisplay();
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
        RefreshDisplay();

        //Debug.Log("A new Barrel Slot has been added for: " + type + "with a quantity of: " + barrelData[barrelData.Count - 1].quantity);
    }

    private void removeItemFromBarrel(ItemType type) 
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
        for (int i = 0; i < UISlots.Length; i++)
        {
            UISlots[i].exists.SetActive(false);

            UISlots[i].itemIcon.GetComponent<Image>().sprite = null;
            UISlots[i].itemIcon.SetActive(false);

            UISlots[i].numberIcon.GetComponentInChildren<TextMeshProUGUI>().text = 0.ToString();
            UISlots[i].numberIcon.SetActive(false);
        }
    }

    private void RefreshDisplay()
    {
        // Loop length of the BarrelSlots
        for (int i = 0; i < barrelData.Count; i++)
        {
            if (barrelData[i].type != ItemType.None)
            {
                UISlots[i].exists.SetActive(true);

                UISlots[i].itemIcon.SetActive(true);
                UISlots[i].itemIcon.GetComponent<Image>().sprite = inventoryManager.getTexture(barrelData[i].type);

                UISlots[i].numberIcon.SetActive(true);
                UISlots[i].numberIcon.GetComponentInChildren<TextMeshProUGUI>().text = barrelData[i].quantity.ToString();
            }
        }

        // Loop the Rest of UI Slots
        for (int i = barrelData.Count; i < UISlots.Length; i++)
        {
            UISlots[i].exists.SetActive(false);

            UISlots[i].itemIcon.GetComponent<Image>().sprite = null;
            UISlots[i].itemIcon.SetActive(false);

            UISlots[i].numberIcon.GetComponentInChildren<TextMeshProUGUI>().text = 0.ToString();
            UISlots[i].numberIcon.SetActive(false);
        }
    }
}
