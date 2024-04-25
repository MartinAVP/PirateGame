
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    #region eventBus
    private void OnEnable()
    {
        GameEventBus.Subscribe(GameEventsType.INTERACT, ItemInteract);
    }
    private void OnDisable()
    {
        GameEventBus.Unsubscribe(GameEventsType.INTERACT, ItemInteract);
    }
    #endregion
    [SerializeField] private InventorySlot[] InventorySlots;
    [SerializeField] private ContainerSlot[] ContainerSlots;
    [SerializeField] private Transform InventoryParent;
    [SerializeField] private Transform container_PlayerInventory;
    [SerializeField] public Transform container_ContainerInventory;

    [Header("Textures for Items")]
    [SerializeField] private Sprite bananaTexture;
    [SerializeField] private Sprite cannonBallTexture;
    [SerializeField] private Sprite plankTexture;

    private playerInteraction playerInt;

    [SerializeField] public BarrelInventory[] UISlots;

    private void Awake()
    {
        playerInt = FindObjectOfType<playerInteraction>();

        UISlots = new BarrelInventory[16];

        #region Init BarrelSlots

        int j = 0;
        foreach (Transform slot in container_ContainerInventory)
        {
            // Creates a new Inventory Slot item;
            UISlots[j] = new BarrelInventory(0, slot.gameObject, null, null, null, null);
            UISlots[j].slot.name = "Container Slot " + (j + 1);

            //Define each child;
            foreach (Transform childItem in slot)
            {
                if (childItem.name == "ItemIcon")
                {
                    childItem.name = "ItemIcon" + j;
                    UISlots[j].itemIcon = childItem.gameObject;
                }
                else if (childItem.name == "NumberSlot")
                {
                    childItem.name = "NumberBackground";
                    UISlots[j].numberIcon = childItem.gameObject;
                }
                else if (childItem.name == "Exists")
                {
                    childItem.name = "Exists" + j;
                    UISlots[j].exists = childItem.gameObject;
                    //print(UISlots[j].exists.name);
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
        InitPlayerInventory();
        ContainerSlots = new ContainerSlot[16];

        int j = 0;
        foreach(Transform slot in container_PlayerInventory)
        {
            ContainerSlots[j] = new ContainerSlot(slot.gameObject, null, null, null, null);
            ContainerSlots[j].slot.name = "Slot " + (j + 1);


            foreach (Transform childItem in slot)
            {
                if (childItem.name == "Item")
                {
                    childItem.name = "ItemIcon";
                    ContainerSlots[j].itemIcon = childItem.gameObject;
                }
                else if (childItem.name == "NumberSlot")
                {
                    childItem.name = "NumberBackground";
                    ContainerSlots[j].numberIcon = childItem.gameObject;
                }
                else if (childItem.name == "Exists")
                {
                    childItem.name = "ItemExistsOutline";
                    ContainerSlots[j].exists = childItem.gameObject;
                }
                else if (childItem.name == "Selected")
                {
                    childItem.name = "ItemSelectedOutline";
                    ContainerSlots[j].selected = childItem.gameObject;
                }

            }
            j++;

        }

/*        for (int i = 0; i < InventorySlots.Length; i++)
        {
            ContainerSlots[i]. = 
        }*/
    }

    private void InitPlayerInventory()
    {
        InventorySlots = new InventorySlot[8];

        int j = 0;
        foreach (Transform slot in InventoryParent)
        {
            // Creates a new Inventory Slot item;
            InventorySlots[j] = new InventorySlot(j, slot.gameObject, false, ItemType.None, false, 0, null, null);
            InventorySlots[j].SlotItem.name = "Slot " + (j + 1);
            InventorySlots[j].SlotItem.GetComponent<Slot>().itemStored = ItemType.None;
            InventorySlots[j].used = false;

            //Define each child;
            foreach (Transform childItem in slot)
            {
                if (childItem.name == "SlotItemIcon")
                {
                    childItem.name = "ItemIcon";
                    InventorySlots[j].icon = childItem.gameObject;
                }
                else if (childItem.name == "NumberVar")
                {
                    childItem.name = "NumberBackground";
                    InventorySlots[j].numberIcon = childItem.gameObject;

                }
            }

            j++;
        }
    }

    public void addItem(ItemType item)
    {
        bool foundItem = false;
        for (int i = 0; i < InventorySlots.Length; i++)
        {
            // Bananas already being saved in Inventory.
            if (InventorySlots[i].itemHeld == item)
            {
                InventorySlots[i].quantity++;
                InventorySlots[i].numberIcon.GetComponentInChildren<TextMeshProUGUI>().text = InventorySlots[i].quantity.ToString();
                //foundItem = true;
                //Debug.Log("There was already a " + item + " instantiated therefore, adding one. New total: " + InventorySlots[i].quantity);
                ContainerSlots[i].numberIcon.GetComponentInChildren<TextMeshProUGUI>().text = InventorySlots[i].quantity.ToString();
                
                foundItem = true;
            }
        }

        // No Bananas in Inventory, using a new slot.
        if (foundItem == false)
        {
            for (int j = 0; j < InventorySlots.Length; j++)
            {
                if (InventorySlots[j].used == false)
                {
                    InventorySlots[j].used = true;

                    InventorySlots[j].icon.SetActive(true);
                    InventorySlots[j].numberIcon.SetActive(true);

                    InventorySlots[j].itemHeld = item;
                    InventorySlots[j].quantity = 1;

                    InventorySlots[j].SlotItem.GetComponent<Slot>().itemStored = item;

                    InventorySlots[j].numberIcon.GetComponentInChildren<TextMeshProUGUI>().text = InventorySlots[j].quantity.ToString();
                    InventorySlots[j].icon.GetComponent<Image>().sprite = getTexture(item);
                    //Debug.Log("The banana is now bein instantiated in the inventory");

                    // Container Side

                    ContainerSlots[j].exists.SetActive(true);
                    ContainerSlots[j].itemIcon.SetActive(true);
                    ContainerSlots[j].exists.SetActive(true);

                    ContainerSlots[j].itemIcon.GetComponent<Image>().sprite = getTexture(item);

                    ContainerSlots[j].numberIcon.SetActive(true);
                    ContainerSlots[j].numberIcon.GetComponentInChildren<TextMeshProUGUI>().text = InventorySlots[j].quantity.ToString();

                    ContainerSlots[j].slot.GetComponent<Slot>().itemStored = item;
                    return;
                }
            }
        }
    }
    public void removeItem(ItemType item)
    {
        bool foundItem = false;
        for (int i = 0; i < InventorySlots.Length; i++)
        {
            // Bananas already being saved in Inventory.
            if (InventorySlots[i].itemHeld == item)
            {
                // There is bananas left in the inventory
                if (InventorySlots[i].quantity > 1) 
                {
                    InventorySlots[i].quantity--;
                    InventorySlots[i].numberIcon.GetComponentInChildren<TextMeshProUGUI>().text = InventorySlots[i].quantity.ToString();

                    ContainerSlots[i].numberIcon.GetComponentInChildren<TextMeshProUGUI>().text = InventorySlots[i].quantity.ToString();
                }
                // There is not any bananas left in the Inventory
                else
                {
                    InventorySlots[i].used = false;

                    InventorySlots[i].icon.SetActive(false);
                    InventorySlots[i].numberIcon.SetActive(false);

                    InventorySlots[i].itemHeld = ItemType.None;

                    InventorySlots[i].SlotItem.GetComponent<Slot>().itemStored = ItemType.None;

                    InventorySlots[i].quantity = 0;
                    InventorySlots[i].numberIcon.GetComponentInChildren<TextMeshProUGUI>().text = InventorySlots[i].quantity.ToString();
                    InventorySlots[i].icon.GetComponent<Image>().sprite = bananaTexture;

                    ContainerSlots[i].slot.SetActive(false);
                    ContainerSlots[i].numberIcon.SetActive(false);

                    ContainerSlots[i].slot.GetComponent<Slot>().itemStored = ItemType.None;

                    ContainerSlots[i].numberIcon.GetComponentInChildren<TextMeshProUGUI>().text = InventorySlots[i].quantity.ToString();
                }

                foundItem = true;
                //Debug.Log("The Banana is already instantiated in the inventory");
                return;
            }
            else
            {
                //Debug.Log("There was no banana in the inventory");
            }
        }

    }
    public Sprite getTexture(ItemType type)
    {
        switch (type)
        {
            case ItemType.None:
                return null;
            case ItemType.Banana:
                return bananaTexture;
            case ItemType.Plank:
                return plankTexture;
            case ItemType.CannonBall:
                return cannonBallTexture;
            default:
                return null;
        }
    }

    private void ItemInteract()
    {
        //print("Interacted");
        if (playerInt.checkObject() == null) { return; }
        //print(playerInt.checkObject().tag);
        if (playerInt.checkObject().tag == "pickUpItem")
        {
            addItem(playerInt.checkObject().GetComponent<PickUpItem>().itemType);
            Destroy(playerInt.checkObject().gameObject);
        }
    }

}



struct InventorySlot
{
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

struct ContainerSlot
{
    public GameObject slot;
    public GameObject itemIcon;
    public GameObject numberIcon;
    public GameObject exists;
    public GameObject selected;
    public ContainerSlot(GameObject slot, GameObject itemIcon, GameObject numberIcon, GameObject exists, GameObject selected)
    {
        this.slot = slot;
        this.itemIcon = itemIcon;
        this.numberIcon = numberIcon;
        this.exists = exists;
        this.selected = selected;
    }
}
