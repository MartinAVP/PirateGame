using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private InventorySlot[] InventorySlots;
    [SerializeField] private Transform InventoryParent;

    private void Start()
    {
        InventorySlots = new InventorySlot[8];

        int j = 0;
        foreach (Transform slot in InventoryParent)
        {
            // Creates a new Inventory Slot item;
            InventorySlots[j] = new InventorySlot(j, slot.gameObject, false, ItemType.None, false, 0);
            InventorySlots[j].SlotItem.name = "Slot " + (j + 1);

            //Define each child;
            foreach (Transform childItem in slot)
            {

            }

            j++;
        }

        //InventorySlot slot1 = new InventorySlot(false, "Slot 1");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("GetBanana"))
            pickUpItem();
    }

    private void pickUpItem()
    {

    }
}

struct InventorySlot
{
    //Variable declaration
    //Note: I'm explicitly declaring them as public, but they are public by default. You can use private if you choose.
    public int id;
    public GameObject SlotItem;
    public bool used;
    ItemType item;
    public bool usesCounter;
    public int quantity;

    //Constructor (not necessary, but helpful)
    public InventorySlot(int id, GameObject SlotItem, bool used, ItemType item, bool usesCounter, int quantity)
    {
        this.id = id;
        this.SlotItem = SlotItem;
        this.used = used;
        this.item = item;
        this.usesCounter = usesCounter;
        this.quantity = quantity;
        
    }
}

public enum ItemType
{
    None,
    Banana,
    CannonBall
}
