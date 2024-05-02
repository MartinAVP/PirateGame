using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventoryManager : MonoBehaviour
{
    public List<InventoryItem> playerInventory;

    private PlayerUIContainerInventory playerContainerInv;
    private playerInteraction playerInt;
    private PlayerQuestTracker quest;

    public UnityEvent inventoryChange;

    private void Awake()
    {
        quest = GetComponent<PlayerQuestTracker>();
        playerContainerInv = GetComponent<PlayerUIContainerInventory>();
        playerInt = GetComponent<playerInteraction>();
        playerInventory = new List<InventoryItem>();

        
    }

    private void Start()
    {
        inventoryChange.AddListener(quest.checkInventory);
    }

    public List<InventoryItem> GetInventory()
    {
        return playerInventory;
    }

    public void addItem(ItemType type)
    {
        bool foundItem = false;
        // Check if its the first value being instantiated.

        for (int i = 0; i < playerInventory.Count; i++)
        {
            // Items already being saved in Inventory.
            if (playerInventory[i].type == type)
            {
                playerInventory[i].quantity++;

                foundItem = true;
            }
        }

        // No Items in Inventory, using a new slot.
        if (foundItem == false)
        {
            InventoryItem newItem = new InventoryItem();
            newItem.quantity = 1;
            newItem.type = type;
            playerInventory.Add(newItem);
        }

        inventoryChange.Invoke();
    }

    public void removeItem(ItemType type)
    {
        for (int i = 0; i < playerInventory.Count; i++)
        {
            // item already being saved in Inventory.
            if (playerInventory[i].type == type)
            {
                // There is items of the same type left in the inventory
                if (playerInventory[i].quantity > 1)
                {
                    playerInventory[i].quantity--;
                }
                // There is not any more items left in the Inventory
                else
                {
                    playerInventory.RemoveAt(i);
                    if(playerInt.currentItemInHand == type)
                    {
                        playerInt.handItem(ItemType.None);
                    }
                }

                inventoryChange.Invoke();
                playerContainerInv.RefreshInventory();
                //Debug.Log("The Banana is already instantiated in the inventory");
                return;
            }
        }

        Debug.LogWarning("There are no more items of type: " + type);
    }

    public bool hasItem(ItemType type)
    {
        foreach (InventoryItem item in playerInventory)
        {
            if (item.type == type)
                return true;
        }

        return false;
    }

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

    private void ItemInteract()
    {
        //print("Interacted");
        if (playerInt.checkObject() == null) { return; }
        //print(playerInt.checkObject().tag);
        if (playerInt.checkObject().tag == "pickUpItem")
        {
            addItem(playerInt.checkObject().GetComponent<PickUpItem>().itemType);
            playerContainerInv.RefreshInventory();
            Destroy(playerInt.checkObject().gameObject);
        }
    }
}