using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContainerUIContainerInventory : MonoBehaviour
{
    private Transform ContainerInventorySlotsTransform;

    //List<UIContainerSlot> playerContainerSlots;
    UIContainerSlot[] containerSlots;

    private BarrelContentManager containerInventory;

    private void Awake()
    {
        containerInventory = GetComponent<BarrelContentManager>();
        ContainerInventorySlotsTransform = FindFirstObjectByType<PlayerUIManager>().containerInventorySlots;
    }

    private void Start()
    {
        containerSlots = new UIContainerSlot[ContainerInventorySlotsTransform.childCount];
        int j = 0;
        foreach (Transform child in ContainerInventorySlotsTransform.transform)
        {
            //print(child.gameObject.name);
            //child.gameObject.name = "Slot " + (j + 1);

            containerSlots[j].slot = child.gameObject;
            containerSlots[j].slot.name = "Slot " + (j + 1);
            // Creates a new Inventory Slot item;
            //Define each child;
            foreach (Transform childItem in containerSlots[j].slot.transform)
            {
                if (childItem.name == "Item")
                {
                    containerSlots[j].itemIcon = childItem.gameObject;
                    childItem.name = "ItemIcon";
                }
                if (childItem.tag == "ExistOverlay")
                {
                    //childItem.name = "ItemIcon";
                    containerSlots[j].exists = childItem.gameObject;
                    containerSlots[j].exists.name = "ExistOverlay " + j;
                }
                if (childItem.tag == "SelectedOverlay")
                {
                    //childItem.name = "ItemIcon";
                    containerSlots[j].selected = childItem.gameObject;
                    containerSlots[j].selected.name = "SelectedOverlay " + j;
                }
                if (childItem.name == "NumberSlot")
                {
                    childItem.name = "NumberBackground";
                    containerSlots[j].numberIcon = childItem.gameObject;

                }
            }

            j++;
        }
    }

    public void RefreshInventory()
    {
        int i = 0;
        foreach (BarrelSlot item in containerInventory.barrelData)
        {
            // Get all items in the player inventory and assign them to the UI

            // Set the Image to the slot icon of the item at the index
            containerSlots[i].itemIcon.SetActive(true);
            containerSlots[i].itemIcon.GetComponent<Image>().sprite = FindObjectOfType<GameAssets>().SearchObject(containerInventory.barrelData[i].type).itemIcon;

            // Set the number icon
            containerSlots[i].numberIcon.SetActive(true);
            containerSlots[i].numberIcon.GetComponentInChildren<TextMeshProUGUI>().text = containerInventory.barrelData[i].quantity.ToString();

            // Set the item exists slot to enabled
            containerSlots[i].exists.SetActive(true);

            // Set the itemStored type in the UI Slot
            containerSlots[i].slot.GetComponent<SlotContainer>().itemStored = containerInventory.barrelData[i].type;

            // Get all the empty slots in the UI and empty them
            i++;
        }

        // Start at the end of the of the first iteration, and null the rest of the items
        for (int j = i; j < containerSlots.Length; j++)
        {
            // Set the Image to null
            containerSlots[j].itemIcon.GetComponent<Image>().sprite = FindObjectOfType<GameAssets>().SearchObject(ItemType.None).itemIcon;
            containerSlots[j].itemIcon.SetActive(false);

            // Set the number icon
            containerSlots[i].numberIcon.GetComponentInChildren<TextMeshProUGUI>().text = 0.ToString();
            containerSlots[i].numberIcon.SetActive(false);

            // Set the item exists slot to disabled
            containerSlots[i].exists.SetActive(false);

            // Set the itemStored type to none in the UI Slot
            containerSlots[i].slot.GetComponent<SlotContainer>().itemStored = ItemType.None;
        }
    }
}
