using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbable : MonoBehaviour
{
    private void OnEnable()
    {
        GameEventBus.Subscribe(GameEventsType.INTERACT, climbLadder);
    }
    private void OnDisable()
    {
        GameEventBus.Unsubscribe(GameEventsType.INTERACT, climbLadder);
    }

    private playerInteraction playerInt;

    private void Awake()
    {
        playerInt = FindObjectOfType<playerInteraction>();
    }

    private void climbLadder()
    {
        //Debug.Log(playerInt.checkObject().ToString());
        if (playerInt.checkObject() == null) { print("nothing"); return; }
        //if (playerInt.checkObject().gameObject != this.gameObject) { return; }
        //Debug.Log(playerInt.checkObject().name);
        if (playerInt.checkObject().gameObject == this.gameObject)
        {
            Debug.Log("Ladder");
        }
    }
}
