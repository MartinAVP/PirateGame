using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameItems : MonoBehaviour
{
    [Header("Prefabs")]
    [Header("FoodItems")]
    public GameObject bananaPrefab;
    [Header("Useables")]
    public GameObject plankPrefab;
    public GameObject cannonBallPrefab;

    public GameObject GetPrefab(ItemType type)
    {
        switch (type)
        {
            case ItemType.None:
                return null;

            case ItemType.Banana:
                return bananaPrefab;

            case ItemType.Plank:
                return plankPrefab;

            case ItemType.CannonBall:
                return cannonBallPrefab;

            default:
                return null;
        }
    }
}

public enum ItemType
{
    None,
    Banana,
    Plank,
    CannonBall
}

public enum InteractType
{
    None,
    Cannon,
    Barrel
}