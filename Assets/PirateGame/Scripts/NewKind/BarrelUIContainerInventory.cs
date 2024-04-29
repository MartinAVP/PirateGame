using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContainerUIContainerInventory : MonoBehaviour
{
    private Transform ContainerInventorySlotsTransform;

    //List<UIContainerSlot> playerContainerSlots;
    //UIContainerSlot[] containerSlots;

    private BarrelContentManager containerInventory;
    private PlayerUIContainerInventory playerInventoryUI;
    private PlayerUIManager playerUIManager;

    private void Awake()
    {
        containerInventory = GetComponent<BarrelContentManager>();
        playerInventoryUI = FindFirstObjectByType<PlayerUIContainerInventory>();
        playerUIManager = FindFirstObjectByType<PlayerUIManager>();
    }

    private void Start()
    {
        ContainerInventorySlotsTransform = FindFirstObjectByType<PlayerUIManager>().containerInventorySlots;
        //Debug.Log(ContainerInventorySlotsTransform.gameObject.name);

        int j = 0;
        foreach (Transform child in ContainerInventorySlotsTransform.transform)
        {
            //print(child.gameObject.name);
            //child.gameObject.name = "Slot " + (j + 1);

            playerUIManager.containerSlots[j].slot = child.gameObject;
            playerUIManager.containerSlots[j].slot.name = "Slot " + (j + 1);
            // Creates a new Inventory Slot item;
            //Define each child;
            foreach (Transform childItem in playerUIManager.containerSlots[j].slot.transform)
            {
                if (childItem.name == "Item")
                {
                    playerUIManager.containerSlots[j].itemIcon = childItem.gameObject;
                    childItem.name = "ItemIcon";
                }
                if (childItem.tag == "ExistOverlay")
                {
                    //childItem.name = "ItemIcon";
                    playerUIManager.containerSlots[j].exists = childItem.gameObject;
                    playerUIManager.containerSlots[j].exists.name = "ExistOverlay " + j;
                }
                if (childItem.tag == "SelectedOverlay")
                {
                    //childItem.name = "ItemIcon";
                    playerUIManager.containerSlots[j].selected = childItem.gameObject;
                    playerUIManager.containerSlots[j].selected.name = "SelectedOverlay " + j;
                }
                if (childItem.name == "NumberSlot")
                {
                    childItem.name = "NumberBackground";
                    playerUIManager.containerSlots[j].numberIcon = childItem.gameObject;

                }
            }

            j++;
        }

        //playerUIManager.containerSlots = containerSlots;

        UpdateBarrelDisplay();
    }

    public void UpdateBarrelDisplay()
    {
        int i = 0;
        foreach (BarrelSlot item in containerInventory.barrelData)
        {
            // Get all items in the player inventory and assign them to the UI
            //Debug.Log("There is an item on slot " + i);

            // Set the Image to the slot icon of the item at the index
            //print(playerUIManager.containerSlots);
            playerUIManager.containerSlots[i].itemIcon.SetActive(true);
            playerUIManager.containerSlots[i].itemIcon.GetComponent<Image>().sprite = FindObjectOfType<GameAssets>().FindItemTypeData(containerInventory.barrelData[i].type).itemIcon;

            // Set the number icon
            playerUIManager.containerSlots[i].numberIcon.SetActive(true);
            playerUIManager.containerSlots[i].numberIcon.GetComponentInChildren<TextMeshProUGUI>().text = containerInventory.barrelData[i].quantity.ToString();

            // Set the item exists slot to enabled
            playerUIManager.containerSlots[i].exists.SetActive(true);

            // Set the itemStored type in the UI Slot
            playerUIManager.containerSlots[i].slot.GetComponent<SlotContainer>().itemStored = containerInventory.barrelData[i].type;

            // Get all the empty slots in the UI and empty them
            i++;
        }

        // Start at the end of the of the first iteration, and null the rest of the items
        for (int j = i; j < playerUIManager.containerSlots.Length; j++)
        {
            // Set the Image to null
            playerUIManager.containerSlots[j].itemIcon.GetComponent<Image>().sprite = FindObjectOfType<GameAssets>().FindItemTypeData(ItemType.None).itemIcon;
            playerUIManager.containerSlots[j].itemIcon.SetActive(false);

            // Set the number icon
            playerUIManager.containerSlots[j].numberIcon.GetComponentInChildren<TextMeshProUGUI>().text = 0.ToString();
            playerUIManager.containerSlots[j].numberIcon.SetActive(false);

            // Set the item exists slot to disabled
            playerUIManager.containerSlots[j].exists.SetActive(false);

            // Set the itemStored type to none in the UI Slot
            playerUIManager.containerSlots[j].slot.GetComponent<SlotContainer>().itemStored = ItemType.None;
        }
    }

    public void OpenBarrel()
    {
        UpdateBarrelDisplay();
    }

    public void CloseBarrel()
    {
        for (int j = 0; j < playerUIManager.containerSlots.Length; j++)
        {
            // Set the Image to null
            playerUIManager.containerSlots[j].itemIcon.GetComponent<Image>().sprite = FindObjectOfType<GameAssets>().FindItemTypeData(ItemType.None).itemIcon;
            playerUIManager.containerSlots[j].itemIcon.SetActive(false);

            // Set the number icon
            playerUIManager.containerSlots[j].numberIcon.GetComponentInChildren<TextMeshProUGUI>().text = 0.ToString();
            playerUIManager.containerSlots[j].numberIcon.SetActive(false);

            // Set the item exists slot to disabled
            playerUIManager.containerSlots[j].exists.SetActive(false);

            // Set the itemStored type to none in the UI Slot
            playerUIManager.containerSlots[j].slot.GetComponent<SlotContainer>().itemStored = ItemType.None;
        }


        // Make each selected Overlay false
        for (int k = 0; k < playerUIManager.containerSlots.Length; k++)
        {
            playerUIManager.containerSlots[k].selected.SetActive(false);
        }
    }
}
