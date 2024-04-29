using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    [SerializeField] public List<item> GameItems;
    [SerializeField] public List<interactable> GameInteractables;

    public static GameAssets instance;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public item FindItemTypeData(ItemType type)
    {
        foreach (item targetItem in GameItems)
        {
            if(targetItem.type == type)
            {
                return targetItem;
            }

        }
        return null;
    }

    public interactable FindInteractableTypeData(InteractType type)
    {
        foreach (interactable targetItem in GameInteractables)
        {
            if (targetItem.type == type)
            {
                return targetItem;
            }

        }
        return null;
    }
}

[System.Serializable]
public class item
{
    public int id;
    public ItemType type;
    public Sprite itemIcon;
    public GameObject handItemPrefab;
    public GameObject toolTip;
}

[System.Serializable]
public class interactable
{
    public int id;
    public InteractType type;
    public GameObject toolTip;
}

public enum ItemType
{
    None,
    CannonBall,
    Banana,
    Plank
}

public enum InteractType
{
    None,
    Cannon,
    Barrel
}
