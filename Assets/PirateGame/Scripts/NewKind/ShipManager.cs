using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    public int shipHealth = 100;
    private int startHealth = 100;
    public GameObject fireParticleEffect;
    public GameObject shipFireLoc1;
    public GameObject sunkShipPrefab;

    private bool isOnFire = false;

    private void Awake()
    {
        startHealth = shipHealth;
    }

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

        if(shipHealth <= startHealth / 2)
        {
            if (!isOnFire)
            {
                isOnFire = true;
                GameObject _tempParticle = Instantiate(fireParticleEffect, shipFireLoc1.transform.position, Quaternion.identity);
                _tempParticle.transform.parent = shipFireLoc1.transform;
            }
        }

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
        Instantiate(sunkShipPrefab, this.transform.position, this.transform.rotation);
        Destroy(gameObject);
    }
}
