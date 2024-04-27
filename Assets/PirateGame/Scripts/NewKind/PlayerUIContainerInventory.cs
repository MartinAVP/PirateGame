using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIContainerInventory : MonoBehaviour
{
    [SerializeField] public Transform PlayerInventorySlotsTransform;

    //List<UIContainerSlot> playerContainerSlots;
    UIContainerSlot[] playerContainerSlots;

    private PlayerInventoryManager inventory;

    private void Awake()
    {
        inventory = GetComponent<PlayerInventoryManager>();
    }

    private void Start()
    {
        playerContainerSlots = new UIContainerSlot[PlayerInventorySlotsTransform.childCount];
        int j = 0;
        foreach (Transform child in PlayerInventorySlotsTransform.transform)
        {
            //print(child.gameObject.name);
            //child.gameObject.name = "Slot " + (j + 1);

            playerContainerSlots[j].slot = child.gameObject;
            playerContainerSlots[j].slot.name = "Slot " + (j + 1);
            // Creates a new Inventory Slot item;
            //Define each child;
            foreach (Transform childItem in playerContainerSlots[j].slot.transform)
            {
                if (childItem.name == "Item")
                {
                    playerContainerSlots[j].itemIcon = childItem.gameObject;
                    childItem.name = "ItemIcon";
                }
                if (childItem.tag == "ExistOverlay")
                {
                    //childItem.name = "ItemIcon";
                    playerContainerSlots[j].exists = childItem.gameObject;
                    playerContainerSlots[j].exists.name = "ExistOverlay " + j;
                }
                if (childItem.tag == "SelectedOverlay")
                {
                    //childItem.name = "ItemIcon";
                    playerContainerSlots[j].selected = childItem.gameObject;
                    playerContainerSlots[j].selected.name = "SelectedOverlay " + j;
                }
                if (childItem.name == "NumberSlot")
                {
                    childItem.name = "NumberBackground";
                    playerContainerSlots[j].numberIcon = childItem.gameObject;

                }
            }

            j++;
        }
    }

    public void RefreshInventory()
    {
        int i = 0;
        foreach(InventoryItem item in inventory.playerInventory)
        {
            // Get all items in the player inventory and assign them to the UI

            // Set the Image to the slot icon of the item at the index
            playerContainerSlots[i].itemIcon.SetActive(true);
            playerContainerSlots[i].itemIcon.GetComponent<Image>().sprite = FindObjectOfType<GameAssets>().SearchObject(inventory.playerInventory[i].type).itemIcon;

            // Set the number icon
            playerContainerSlots[i].numberIcon.SetActive(true);
            playerContainerSlots[i].numberIcon.GetComponentInChildren<TextMeshProUGUI>().text = inventory.playerInventory[i].quantity.ToString();

            // Set the item exists slot to enabled
            playerContainerSlots[i].exists.SetActive(true);

            // Set the itemStored type in the UI Slot
            playerContainerSlots[i].slot.GetComponent<SlotContainer>().itemStored = inventory.playerInventory[i].type;

            // Get all the empty slots in the UI and empty them
            i++;
        }

        // Start at the end of the of the first iteration, and null the rest of the items
        for (int j = i; j < playerContainerSlots.Length; j++)
        {
            // Set the Image to null
            playerContainerSlots[j].itemIcon.GetComponent<Image>().sprite = FindObjectOfType<GameAssets>().SearchObject(ItemType.None).itemIcon;
            playerContainerSlots[j].itemIcon.SetActive(false);

            // Set the number icon
            playerContainerSlots[i].numberIcon.GetComponentInChildren<TextMeshProUGUI>().text = 0.ToString();
            playerContainerSlots[i].numberIcon.SetActive(false);

            // Set the item exists slot to disabled
            playerContainerSlots[i].exists.SetActive(false);

            // Set the itemStored type to none in the UI Slot
            playerContainerSlots[i].slot.GetComponent<SlotContainer>().itemStored = ItemType.None;
        }
    }
}
