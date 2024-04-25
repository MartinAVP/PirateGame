using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotContainer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public ItemType itemStored;
    private playerInteraction playerInt;
    private InventoryManager playerInv;
    private GameObject selected;
    [SerializeField]private bool isContainer;
    [SerializeField] private bool isPlayerContainer;

    private void Start()
    {
        playerInt = FindObjectOfType<playerInteraction>();
        playerInv = FindObjectOfType<InventoryManager>();
        selected = gameObject.transform.Find("Selected").gameObject;
    }

    public void selectItem()
    {
        // Check if the Container Slot is empty
        if(itemStored != ItemType.None)
        {
            // Check if the script is a ContainerSlot
            if (isContainer)
            {
                //Debug.Log("Button Pressed and Item is Container");
                playerInv.GrabFromContainer(itemStored);
            }
            
            if(isPlayerContainer)
            {
                playerInv.PlaceOnContainer(itemStored);
            }

            // Check if the script is in a PlayerSlot
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //mouse_over = true;
        selected.SetActive(true);
        Debug.Log("Mouse enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //mouse_over = false;
        selected.SetActive(false);
        //this.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
        Debug.Log("Mouse exit");
    }
}
