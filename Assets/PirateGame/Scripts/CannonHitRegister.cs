using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonHitRegister : MonoBehaviour
{
    [SerializeField] private float timeBetweenHitSound = 90;
    [SerializeField] private float currentTime;

    private List<GameObject> 

    private void Start()
    {
        currentTime = timeBetweenHitSound;
    }

    private void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
    }

    private void newCannonBall(GameObject cannonBall)
    {
        //
    }
}
