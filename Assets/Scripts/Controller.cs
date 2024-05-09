using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] MoveAlongSpline moveAlong;

    private void Awake()
    {
        moveAlong.speed = 0;
    }

    private void OnGUI()
    {
        if(GUILayout.Button("Start Movement"))
        {
            moveAlong.speed = 5f;
        }
        if (GUILayout.Button("Increase Movement"))
        {
            moveAlong.speed += 3f;
        }
        if (GUILayout.Button("Decrease Movement"))
        {
            moveAlong.speed += 3f;

            if(moveAlong.speed <= 1)
            {
                moveAlong.speed = 0;
            }
        }
        if (GUILayout.Button("Stop Movement"))
        {
            moveAlong.speed = 0;
        }
    }
}
