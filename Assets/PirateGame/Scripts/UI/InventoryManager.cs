
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
    [SerializeField] private Transform InventoryParent;

    [Header("Textures for Items")]
    [SerializeField] private Sprite bananaTexture;
    [SerializeField] private Sprite cannonBallTexture;
    [SerializeField] private Sprite plankTexture;

    private playerInteraction playerInt;

    private void Awake()
    {
        playerInt = FindObjectOfType<playerInteraction>();
    }

    private void Start()
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
/*    private void OnGUI()
    {
        if (GUILayout.Button("Grab Banana"))
            addItem(ItemType.Banana);
        if (GUILayout.Button("Drop/Use Banana"))
            removeItem(ItemType.Banana);
        if (GUILayout.Button("Grab Coconut"))
            addItem(ItemType.Coconut);
        if (GUILayout.Button("Drop/Use Coconut"))
            removeItem(ItemType.Coconut);
        if (GUILayout.Button("Grab Cannon Ball"))
            addItem(ItemType.CannonBall);
        if (GUILayout.Button("Drop/Use Cannon Ball"))
            removeItem(ItemType.CannonBall);
    }*/

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
    private Sprite getTexture(ItemType type)
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
