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

    private void Awake()
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
    }
}
