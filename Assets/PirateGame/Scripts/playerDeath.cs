using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDeath : MonoBehaviour
{
    private Vector3 startPosition;
    private Quaternion startOrientation;

    [SerializeField] private float playerHeight = 0;

    private void Awake()
    {
        startPosition = transform.position;
        startOrientation = transform.rotation;
    }

    private void FixedUpdate()
    {
        if(transform.position.y <= playerHeight)
        {
            respawn();
        }
    }

    public void respawn()
    {
        print("Out of Bounds");
        this.transform.position = startPosition;
        this.transform.rotation = startOrientation;
    }
}
