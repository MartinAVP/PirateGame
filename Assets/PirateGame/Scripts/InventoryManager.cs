using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private GameObject[] InventorySlots = new GameObject[8];
    [SerializeField] private Transform InventoryParent;

    private void Start()
    {
        int i = 0;
        foreach (GameObject slot in InventoryParent)
        {
            InventorySlots[i] = slot;
        }
    }
}
