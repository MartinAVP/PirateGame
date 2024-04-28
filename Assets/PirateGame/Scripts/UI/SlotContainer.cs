using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotContainer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private void OnEnable()
    {
        GameEventBus.Subscribe(GameEventsType.INTERACT, updateBarrelLookingAt);
    }
    private void OnDisable()
    {
        GameEventBus.Unsubscribe(GameEventsType.INTERACT, updateBarrelLookingAt);
    }

    public ItemType itemStored;

    private PlayerInventoryManager playerInventory; // Change the Inventory for the player
    private PlayerUIContainerInventory inventoryUI; // Update the inventory of the Player
    private BarrelContentManager barrelContent; // Change the inventory of the Barrel
    private ContainerUIContainerInventory containerUI; // Update the inventory of the Barrel

    private playerInteraction playerInt; // Get the barrel the player is looking at

    private GameObject selected;
    [SerializeField]private bool isContainer;
    [SerializeField] private bool isPlayerContainer;

    private void Start()
    {
        playerInventory = FindFirstObjectByType<PlayerInventoryManager>();
        inventoryUI = FindFirstObjectByType<PlayerUIContainerInventory>();

        playerInt = FindFirstObjectByType<playerInteraction>();

        //selected = gameObject.transform.;

        foreach (Transform child in this.gameObject.transform)
        {
            if (child.tag == "SelectedOverlay")
            {
                selected = child.gameObject;
            }
        }
    }


    //public Transform myObj;
    private void updateBarrelLookingAt()
    {
        barrelContent = playerInt.checkObject().transform.parent.GetComponent<BarrelContentManager>();
        containerUI = playerInt.checkObject().transform.parent.GetComponent<ContainerUIContainerInventory>();

        
/*        print(playerInt.checkObject().transform);
        myObj = playerInt.checkObject().transform;*/
    }

    public void selectItem()
    {
        // Check if the Container Slot is empty
        if(itemStored != ItemType.None)
        {
            // Check if the script is a ContainerSlot
            if (isContainer)
            {
                // Item is in container, Remove from container add to Player Inventory
                barrelContent.removeItemFromBarrel(itemStored);
                playerInventory.addItem(itemStored);
            }
            
            if(isPlayerContainer)
            {
                // Item is in Player Inventory, Add to Container, remove from Player Inventory
                playerInventory.removeItem(itemStored);
                barrelContent.AddItemToBarrel(itemStored);
            }

            // Update the UI for Both Barrel and Player inventories
            inventoryUI.RefreshInventory();
            containerUI.UpdateBarrelDisplay();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //mouse_over = true;
        if(itemStored != ItemType.None)
            selected.SetActive(true);
        //Debug.Log("Mouse enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //mouse_over = false;
        selected.SetActive(false);
        //this.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
        //Debug.Log("Mouse exit");
    }
}
