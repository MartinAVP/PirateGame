using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] public Transform playerCanvas;

    // NOTE: The Canvas Names are very important

    // Panels
    [HideInInspector] public Transform mainLayout;
    [HideInInspector] public Transform playerWheel;
    [HideInInspector] public Transform containerInventory;

    // Container Positions
    [HideInInspector] public Transform containerPlayerInventorySlots;
    [HideInInspector] public Transform containerInventorySlots;

    // Player Wheel Positions
    [HideInInspector] public Transform playerWheelSlots;

    [HideInInspector]public UIContainerSlot[] containerSlots;

    private void Awake()
    {
        containerSlots = new UIContainerSlot[16];
    }

    private void OnEnable()
    {
        // Find the Panels
        foreach (Transform canvasChild in playerCanvas)
        {
            if (canvasChild.name == "MainLayout")
                mainLayout = canvasChild;
            else if (canvasChild.name == "PlayerWheel")
                playerWheel = canvasChild;
            else if(canvasChild.name == "PlayerContainerInventory")
                containerInventory = canvasChild;
        }

        // Find the Container Positions
        foreach(Transform containerInventoryChild in containerInventory)
        {
            if(containerInventoryChild.name == "PlayerInventorySlots")
                containerPlayerInventorySlots = containerInventoryChild;
            else if(containerInventoryChild.name == "ContainerInventorySlots")
                containerInventorySlots = containerInventoryChild;
        }

        // Find the PlayerWheel Positions
        foreach (Transform playerWheelChild in playerWheel)
        {
            if (playerWheelChild.name == "PlayerQuickInventory")
                playerWheelSlots = playerWheelChild;
        }

    }

    // Inventory Wheel Public Functions
    public void openInventoryWheel()
    {
        playerWheel.gameObject.SetActive(true);
        EnableCursor();
    }

    public void closeInventoryWheel()
    {
        playerWheel.gameObject.SetActive(false);
        DisableCursor();
    }

    // Container Inventory Public Functions
    public void openContainerInventory()
    {
        containerInventory.gameObject.SetActive(true);
        EnableCursor();
    }

    public void closeContainerInventory()
    {
        containerInventory.gameObject.SetActive(false);
        DisableCursor();
    }

    private void EnableCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void DisableCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
