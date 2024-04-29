using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTooltips : MonoBehaviour
{
    [Header("Items")]
    [SerializeField] GameObject cannonBallToolTip;
    [SerializeField] GameObject bananaToolTip;
    [SerializeField] GameObject plankToolTip;

    [Header("Interactables")]
    [SerializeField] GameObject cannonToolTip;
    [SerializeField] GameObject barrelToolTip;
    public GameObject GetItemTooltip(ItemType type)
    {
        switch (type)
        {
            case ItemType.None:
                return null;

            case ItemType.Banana:
                return bananaToolTip;

            case ItemType.Plank:
                return plankToolTip;

            case ItemType.CannonBall:
                return cannonBallToolTip;

            default:
                return null;
        }
    }

    public GameObject GetInteractTooltip(InteractType type)
    {
        switch (type)
        {
            case InteractType.None:
                return null;
                break;
            case InteractType.Cannon:
                return cannonToolTip;
                break;
            case InteractType.Barrel:
                return barrelToolTip;
            default:
                return null;
        }
    }
}
