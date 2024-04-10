using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class playerInteraction : MonoBehaviour
{
    [SerializeField]private Transform CameraPos;
    [SerializeField]private Transform CameraLoc;
    [SerializeField]private bool canInteract = true;
    [SerializeField][Range(0.1f, 1)] private float interactDelay = .1f;

    private bool usingCannon;
    private void Start()
    {
        CameraPos = Camera.main.transform;
    }

    public Transform getCameraPos(){ return CameraLoc; }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)) 
        {
            if (canInteract)
            {
                canInteract = false;
                StartCoroutine(newInteractDelay());
                //checkObject();
                //if(checkObject() == null) { return; }
                GameEventBus.Publish(GameEventsType.INTERACT);
            }
        }
    }

    private IEnumerator newInteractDelay()
    {
        canInteract = true;
        yield return new WaitForSeconds(interactDelay);
    }

    public GameObject checkObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(CameraPos.position, CameraPos.transform.forward, out hit, 5f))
        {
            //print(hit.transform.name);
            if (hit.transform.gameObject != null)
            {
                return hit.transform.gameObject;
            }
            else if(hit.transform.gameObject == null) { return null; }
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
