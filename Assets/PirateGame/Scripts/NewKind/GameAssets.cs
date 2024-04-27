using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    [SerializeField] public List<item> GameItems;

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

    public item SearchObject(ItemType type)
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
}

[System.Serializable]
public class item
{
    public int id;
    public ItemType type;
    public Sprite itemIcon;
    public GameObject handItemPrefab;
    public GameObject toolTip;

/*    public item(int id, ItemType type, Sprite itemIcon, GameObject handItemPrefab, GameObject toolTip)
    {
        this.id = id;
        this.type = type;
        this.itemIcon = itemIcon;
        this.handItemPrefab = handItemPrefab;
        this.toolTip = toolTip;
    }*/
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
