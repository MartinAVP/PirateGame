using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BarrelContentManager : MonoBehaviour
{
    public List<BarrelSlot> barrelData;

    [Serializable]
    public struct inputValues
    {
        public int quantity;
        public ItemType type;
    }

    public inputValues[] startValues;

    private void Awake()
    {
        // Initialize BarrelData List
        barrelData = new List<BarrelSlot>();
    }

    private void Start()
    {
        // Initialize the values added in the inspector to the barrel Contents
        foreach (inputValues value in startValues)
        {
            for (int i = 0; i < value.quantity; i++)
            {
                AddItemToBarrel(value.type);
            }
        }

    }

    public void AddItemToBarrel(ItemType type)
    {
        // Check if the Object already Exists
        for (int i = 0; i < barrelData.Count; i++)
        {
            if (barrelData[i].type == type)
            {
                BarrelSlot tmpBarrel = barrelData[i];
                tmpBarrel.quantity++;
                barrelData[i] = tmpBarrel;

                barrelData[i].addQuantity(1);
                //Debug.Log("The Barrel already contains " + type + " so increasing the quantity to " + barrelData[i].quantity);
                //RefreshDisplay();
                return;
            }
        }

        // If Object Doesn't Exist then Add a new Object to the List;

        //Check if its the first Object to be added to the Barrel;
        if (barrelData.Count == 0)
        {
            barrelData.Add(new BarrelSlot(0, 1, type));
        }
        else
        {
            barrelData.Add(new BarrelSlot(barrelData.Count, 1, type));
        }
        //RefreshDisplay();

        //Debug.Log("A new Barrel Slot has been added for: " + type + "with a quantity of: " + barrelData[barrelData.Count - 1].quantity);
    }

    public void removeItemFromBarrel(ItemType type)
    {
        // Check if the object being removed quantity is more than 0
        for (int i = 0; i < barrelData.Count; i++)
        {
            if (barrelData[i].type == type)
            {
                // Check if the barrel slot holds more than one item
                if (barrelData[i].quantity > 1)
                {
                    // Extract the barrelData and change the quantity to later return it
                    BarrelSlot tmpBarrel = barrelData[i];
                    int prevQuantity = tmpBarrel.quantity;
                    tmpBarrel.quantity--;
                    barrelData[i] = tmpBarrel;

                    barrelData[i].removeQuantity(1);
                    //Debug.Log("The Barrel has " + prevQuantity + " of " + type + "; The new total is " + barrelData[i].quantity);
                    //RefreshDisplay();
                    return;
                }
                // Else remove the object from the List
                else
                {
                    // Extract the barrelData and change the quantity to 0
                    BarrelSlot tmpBarrel = barrelData[i];
                    tmpBarrel.quantity = 0;
                    barrelData[i] = tmpBarrel;

                    barrelData.RemoveAt(i);
                    //Debug.Log("The Barrel no longer holds " + type);
                    //barrelData.Sort((x, y) => y.quantity.CompareTo(x.quantity));
                    //RefreshDisplay();
                }
            }
        }
    }
}

public struct BarrelSlot
{
    public int id;
    public int quantity;
    public ItemType type;

    public BarrelSlot(int id, int quantity, ItemType type)
    {
        this.id = id;
        this.quantity = quantity;
        this.type = type;
    }

    public void addQuantity(int quantityAdded)
    {
        quantity += quantityAdded;
    }

    public void removeQuantity(int quantityRemoved)
    {
        quantity -= quantityRemoved;
    }
}