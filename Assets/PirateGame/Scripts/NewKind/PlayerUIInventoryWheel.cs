using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIInventoryWheel : MonoBehaviour
{
    private Transform PlayerInventorySlotsTransform;

    //List<UIContainerSlot> playerContainerSlots;
    [SerializeField] public UIPlayerWheelSlot[] playerContainerSlots;

    private PlayerInventoryManager inventory;


    private void Awake()
    {
        inventory = GetComponent<PlayerInventoryManager>();
    }

    private void Start()
    {
        print(GetComponent<PlayerUIManager>().playerWheelSlots.transform);
        PlayerInventorySlotsTransform = GetComponent<PlayerUIManager>().playerWheelSlots.transform;

        playerContainerSlots = new UIPlayerWheelSlot[GetComponent<PlayerUIManager>().playerWheelSlots.childCount];

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
                    childItem.name = "ItemIcon " + j;
                    playerContainerSlots[j].itemIcon = childItem.gameObject;
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
        foreach (InventoryItem item in inventory.playerInventory)
        {
            // Get all items in the player inventory and assign them to the UI

            // Set the Image to the slot icon of the item at the index
            playerContainerSlots[i].itemIcon.SetActive(true);
            playerContainerSlots[i].itemIcon.GetComponent<Image>().sprite = FindObjectOfType<GameAssets>().FindItemTypeData(inventory.playerInventory[i].type).itemIcon;

            // Set the number icon
            playerContainerSlots[i].numberIcon.SetActive(true);
            playerContainerSlots[i].numberIcon.GetComponentInChildren<TextMeshProUGUI>().text = inventory.playerInventory[i].quantity.ToString();

            // Set the itemStored type in the UI Slot
            playerContainerSlots[i].slot.GetComponent<Slot>().itemStored = inventory.playerInventory[i].type;

            // Get all the empty slots in the UI and empty them
            i++;
        }

        for (int j = i; j < playerContainerSlots.Length; j++)
        {
            // Set the Image to null
            playerContainerSlots[j].itemIcon.GetComponent<Image>().sprite = FindObjectOfType<GameAssets>().FindItemTypeData(ItemType.None).itemIcon;
            playerContainerSlots[j].itemIcon.SetActive(false);

            // Set the number icon
            playerContainerSlots[i].numberIcon.GetComponentInChildren<TextMeshProUGUI>().text = 0.ToString();
            playerContainerSlots[i].numberIcon.SetActive(false);

            // Set the itemStored type to none in the UI Slot
            playerContainerSlots[i].slot.GetComponent<Slot>().itemStored = ItemType.None;
        }
    }

    private void Condense()
    {

    }
}
