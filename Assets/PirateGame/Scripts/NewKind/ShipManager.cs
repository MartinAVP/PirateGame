using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    public int shipHealth = 100;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.GetComponent<CannonBall>() != null)
        {
            shipHealth -= collision.transform.GetComponent<CannonBall>().damage;
        }
    }

    public void takeDamage(int damage)
    {
        shipHealth -= damage;

        if (shipHealth < 0)
        {
            sinkShip();
        }
    }

    private void sinkShip()
    {
        shipHealth = 0;
        GameEventBus.Publish(GameEventsType.SHIPSUNK);
        print("ShipSunk");
        Destroy(gameObject);
    }
}
