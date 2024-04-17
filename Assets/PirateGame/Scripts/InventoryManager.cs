
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private InventorySlot[] InventorySlots;
    [SerializeField] private Transform InventoryParent;

    [Header("Textures for Items")]
    [SerializeField] private Sprite bananaTexture;

    private void Start()
    {
        InventorySlots = new InventorySlot[8];

        int j = 0;
        foreach (Transform slot in InventoryParent)
        {
            // Creates a new Inventory Slot item;
            InventorySlots[j] = new InventorySlot(j, slot.gameObject, false, ItemType.None, false, 0, null, null);
            InventorySlots[j].SlotItem.name = "Slot " + (j + 1);
            InventorySlots[j].used = false;

            //Define each child;
            foreach (Transform childItem in slot)
            {
                if(childItem.name == "SlotItemIcon")
                {
                    childItem.name = "ItemIcon";
                    InventorySlots[j].icon = childItem.gameObject;
                }
                else if(childItem.name == "NumberVar")
                {
                    childItem.name = "NumberBackground";
                    InventorySlots[j].numberIcon = childItem.gameObject;

                }
            }

            j++;
        }

        //InventorySlot slot1 = new InventorySlot(false, "Slot 1");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("GetBanana"))
            addItem(ItemType.Banana);
    }

    private void addItem(ItemType item)
    {
        bool foundItem = false;
        for (int i = 0; i < InventorySlots.Length; i++)
        {
            // Bananas already being saved in Inventory.
            if (InventorySlots[i].itemHeld == ItemType.Banana)
            {
                InventorySlots[i].quantity++;
                InventorySlots[i].numberIcon.GetComponentInChildren<TextMeshProUGUI>().text = InventorySlots[i].quantity.ToString();

                foundItem = true;
                Debug.Log("The Banana is already instantiated in the inventory");
                return;
            }
            // No Bananas in Inventory, using a new slot.
            if(foundItem == false)
            {
                for (int j = 0; j < InventorySlots.Length; j++)
                {
                    if (InventorySlots[j].used == false)
                    {
                        InventorySlots[j].used = true;

                        InventorySlots[j].icon.SetActive(true);
                        InventorySlots[j].numberIcon.SetActive(true);

                        InventorySlots[j].itemHeld = ItemType.Banana;
                        InventorySlots[j].quantity = 1;
                        InventorySlots[j].numberIcon.GetComponentInChildren<TextMeshProUGUI>().text = InventorySlots[i].quantity.ToString();
                        InventorySlots[j].icon.GetComponent<Image>().sprite = bananaTexture;
                        Debug.Log("The banana is now bein instantiated in the inventory");
                        return;
                    }
                }
            }
        }
    }
}

struct InventorySlot
{
    //Variable declaration
    //Note: I'm explicitly declaring them as public, but they are public by default. You can use private if you choose.
    public int id;
    public GameObject SlotItem;
    public bool used;
    public ItemType itemHeld;
    public bool usesCounter;
    public int quantity;

    public GameObject icon;
    public GameObject numberIcon;

    //Constructor (not necessary, but helpful)
    public InventorySlot(int id, GameObject SlotItem, bool used, ItemType item, bool usesCounter, int quantity, GameObject icon, GameObject numberIcon)
    {
        this.id = id;
        this.SlotItem = SlotItem;
        this.used = used;
        this.itemHeld = item;
        this.usesCounter = usesCounter;
        this.quantity = quantity;
        
        this.icon = icon;
        this.numberIcon = numberIcon;   
    }
}

public enum ItemType
{
    None,
    Banana,
    CannonBall
}
