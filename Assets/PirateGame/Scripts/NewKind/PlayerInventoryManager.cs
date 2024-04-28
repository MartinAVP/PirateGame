using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PlayerInventoryManager : MonoBehaviour
{
    public List<InventoryItem> playerInventory;

    private PlayerUIContainerInventory playerContainerInv;

    private void Awake()
    {
        playerContainerInv = GetComponent<PlayerUIContainerInventory>();
        playerInventory = new List<InventoryItem>();
    }

/*    private void OnDisable()
    {
        if(playerInventory.Count != 0 )
        {
            playerInventory.Clear();
        }
    }*/

    private void OnGUI()
    {
        if (GUILayout.Button("PrintList"))
        {
            playerContainerInv.RefreshInventory();
        }
        // Add Items
        else if (GUILayout.Button("Add Cannon Ball Item"))
        {
            addItem(ItemType.CannonBall);
        }
        else if (GUILayout.Button("Add Banana Item"))
        {
            addItem(ItemType.Banana);
        }
        else if (GUILayout.Button("Add Plank Item"))
        {
            addItem(ItemType.Plank);
        }

        // Remove Items
        else if (GUILayout.Button("Remove Cannon Ball Item"))
        {
            removeItem(ItemType.CannonBall);
        }
        else if (GUILayout.Button("Remove Banana Item"))
        {
            removeItem(ItemType.Banana);
        }
        else if (GUILayout.Button("Remove Plank Item"))
        {
            removeItem(ItemType.Plank);
        }

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
                }

                playerContainerInv.RefreshInventory();
                //Debug.Log("The Banana is already instantiated in the inventory");
                return;
            }
        }

        Debug.LogWarning("There are no more items of type: " + type);
    }
}