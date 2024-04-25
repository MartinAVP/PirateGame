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
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        //Debug.Log("Open Inventory");
    }

    public void closeContainerInventory()
    {
        ContainerInventoryPanel.gameObject.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        //Debug.Log("Close Inventory");
    }

    public void openInventory()
    {
        InventoryPanel.gameObject.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void closeInventory()
    {
        InventoryPanel.gameObject.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
