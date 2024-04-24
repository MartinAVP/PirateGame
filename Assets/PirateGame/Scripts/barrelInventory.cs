using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;
using System.Linq;

public class barrelInventory : MonoBehaviour
{
    [SerializeField] private BarrelSlot[] slotData;
    [SerializeField] private BarrelInventory[] UISlots;
    [SerializeField] private Transform SlotsParent;
    //[SerializeField] private Transform[] slotLocations;
    [SerializeField] private LinkedList<BarrelSlot> inventoryOrder;
    // Start is called before the first frame update

    private InventoryManager inventoryManager;

    private void OnDisable()
    {
        //slotLocations = null;
        slotData = null;
        UISlots = null;
    }

    private void Awake()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    private void OnGUI()
    {
        if (GUILayout.Button("OpenBarrel"))
        {
            RefreshBarrel();
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

    private void addItem1()
    {
        AddItemToBarrel(ItemType.Banana);
        RefreshBarrel();
    }

    private void addItem2()
    {
        AddItemToBarrel(ItemType.CannonBall);
        RefreshBarrel();
    }

    private void addItem3()
    {
        AddItemToBarrel(ItemType.Plank);
        RefreshBarrel();
    }

    void Start()
    {
        slotData = new BarrelSlot[16];
        //slotLocations = new Transform[16];
        UISlots = new BarrelInventory[16];
        inventoryOrder = new LinkedList<BarrelSlot>();

        /*        int i = 0;
                foreach(Transform slot in SlotsParent)
                {
                    slot.transform.name = "Container Slot " + (i + 1);
                    slotLocations[i++] = slot;
                }*/
        // Define the Slots Data for the Barrel
        int i = 0;
        foreach(BarrelSlot slot in slotData)
        {
            slotData[i] = new BarrelSlot(i, 0, ItemType.None);
            i++;
        }


        // Define for the Slots in the UI.
        int j = 0;
        foreach (Transform slot in SlotsParent)
        {
            // Creates a new Inventory Slot item;
            UISlots[j] = new BarrelInventory(0, slot.gameObject, null, null, null, null);
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
    }

    public void AddItemToBarrel(ItemType type) 
    {
        int i = 0;
        int j = 0;
        foreach(BarrelSlot slot in slotData)
        {
            if(slot.type == type)
            {
                slotData[i].quantity++;

                //Debug.Log(inventoryOrder.ElementAt(i).type + "    " + inventoryOrder.ElementAt(i).quantity);
                //Debug.Log(inventoryOrder.Find(slot).ToString());

/*                BarrelSlot targetSlot = inventoryOrder.ElementAt(i);
                targetSlot.quantity++;*/

/*                foreach (BarrelSlot slotInLL in inventoryOrder)
                {
                    if(slotInLL.type == type)
                    {
                        BarrelSlot targetSlot = slotInLL;
                        targetSlot.quantity++;
                    }
                    j++;
                }*/

                RefreshBarrel();
                return;
            }
            i++;
        }

        for (int k = 0; k < slotData.Length; k++)
        {
            if (slotData[k].quantity == 0)
            {
                slotData[k].type = type;
                slotData[k].quantity++;
                break;
            }
        }


        Debug.Log("Adding a new Item");
        inventoryOrder.AddLast(new BarrelSlot(0, 1, type));
        
        SortByOrder();
        RefreshBarrel();

        //SortInventoryByQuantity();
    }

    public void removeItemFromBarrel(ItemType type)
    {
        for (int i = 0; i < slotData.Length; i++)
        {
            if (slotData[i].type == type)
            {
                Debug.Log("Found Item at" + slotData[i].id);
                if (slotData[i].quantity >= 1)
                {
                    slotData[i].quantity--;
                }
                if (slotData[i].quantity == 0)
                {
                    slotData[i].quantity = 0;
                    slotData[i].type = ItemType.None;

                    UISlots[i].exists.SetActive(false);

                    UISlots[i].itemIcon.GetComponent<Image>().sprite = null;
                    UISlots[i].itemIcon.SetActive(false);

                    UISlots[i].numberIcon.GetComponentInChildren<TextMeshProUGUI>().text = slotData[i].quantity.ToString();
                    UISlots[i].numberIcon.SetActive(false);

                    inventoryOrder.Remove(slotData[i]);
                    //SortByOrder();
                }

                Debug.Log(slotData[i].quantity);
                RefreshBarrel();
                return;
            }
        }
/*        foreach (BarrelSlot slot in slotData)
        {
            if(slot.type == type)
            {
                Debug.Log("Found Item at" + slot.id);
                foundItemID = slot.id;
                if (slotData[foundItemID].quantity > 0)
                {
                    slotData[foundItemID].quantity--;
                }
                else
                {
                    slotData[foundItemID].quantity = 0;
                    slotData[foundItemID].type = ItemType.None;
                }
                RefreshBarrel();
                return;
            }
*//*            else
            {
                Debug.LogWarning("Item Not Found");
            }*//*
        }*/

    }

    private void RefreshBarrel()
    {
        // Sort Barrel Data
        //SortInventoryByQuantity();

        // Get The Barrel Info;
        int i = 0;
        foreach(BarrelSlot slot in slotData)
        {
            // Quantity is more than 0;
            if (slotData[i].quantity > 0)
            {
                UISlots[i].exists.SetActive(true);

                UISlots[i].itemIcon.SetActive(true);
                UISlots[i].itemIcon.GetComponent<Image>().sprite = inventoryManager.getTexture(slotData[i].type);

                UISlots[i].numberIcon.SetActive(true);
                UISlots[i].numberIcon.GetComponentInChildren<TextMeshProUGUI>().text = slotData[i].quantity.ToString();

            }
            i++;
        }
    }

    private void SortByOrder()
    {
       inventoryOrder.CopyTo(slotData, 0);
    }

    private void SortInventoryByQuantity()
    {
        Array.Sort(slotData, (x, y) => y.quantity.CompareTo(x.quantity));
/*        bool emptyBarrel = true;
        int i = 0;
        // Check if Barrel is Empty
        foreach(BarrelSlot slot in slotData)
        {
            if(slot.quantity == 0)
            {
                emptyBarrel = true;
                Debug.Log("Empty Barrel");
            }
            else
            {
                emptyBarrel = false;
                Debug.Log("Barrel has one item");
                break;
            }
            i++;
        }

        //Debug.Log(emptyBarrel);

        if(emptyBarrel == false)*/
        //{
            
            /*            Debug.Log(slotData[0].quantity);
                        Array.Sort()
                        // While the first slot is empty
                        while (slotData[0].quantity == 0)
                        {
                            Debug.Log("Slot 0 quantity is still 0");
                            // Moving Up Variable
                            for (int i = 0; i < slotData.Length; i++)
                            {
                                // Check if the quantity of item is 0
                                if (slotData[i].quantity == 0)
                                {
                                    if (i == slotData.Length - 1)
                                    {
                                        break;
                                    }
                                    // Check if the next quantity item is more than 0
                                    if (slotData[i + 1].quantity > 0)
                                    {
                                        Debug.Log("Moving: " + i + 1 + "To" + i);
                                        slotData[i] = slotData[i + 1];

                                        // Set i + 1 = empty
                                        slotData[i + 1].quantity = 0;
                                        slotData[i + 1].type = ItemType.None; 
                                    }
                                }
                            }
                        }*/
        //}
    }
}

struct BarrelInventory
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
}
