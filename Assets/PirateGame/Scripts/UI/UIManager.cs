using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] Transform MainPanel;
    [SerializeField] Transform InventoryPanel;
    [SerializeField] Transform ContainerInventoryPanel;


    private InventoryManager inventoryManager;
    private void Awake()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    public void openContainerInventory()
    {
        ContainerInventoryPanel.gameObject.SetActive(true);
        EnableCursor();
    }

    public void closeContainerInventory()
    {
        ContainerInventoryPanel.gameObject.SetActive(false);
        DisableCursor();
    }

    public void openInventory()
    {
        InventoryPanel.gameObject.SetActive(true);
        EnableCursor();
    }

    public void closeInventory()
    {
        InventoryPanel.gameObject.SetActive(false);
        EnableCursor();
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
