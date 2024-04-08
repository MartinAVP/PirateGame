using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class playerInteraction : MonoBehaviour
{
    private Transform CameraPos;

    private void Start()
    {
        CameraPos = Camera.main.transform;
        RaycastHit hit;


    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)) 
        {
            checkObject();
            GameEventBus.Publish(GameEventsType.INTERACT);
        }
    }

    public GameObject checkObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(CameraPos.position, CameraPos.transform.forward, out hit, 5f))
        {
            if(hit.transform.gameObject != null)
            {
                return hit.transform.gameObject;
            }
            else { return null; }
        }

        return null;
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Check Object"))
            checkObject();
    }
}
