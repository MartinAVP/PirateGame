using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [Header("Tooltips")]
    [SerializeField] GameObject cannonToolTip;
    [SerializeField] GameObject bananaToolTip;
    [SerializeField] GameObject coconutToolTip;
    private playerInteraction playerInt;

    private GameObject itemLookedAt;
    private Transform toolTipLoc;

    public GameObject currentMessage;

    private bool messageDisplayed = false;

    private void Start()
    {
        playerInt = FindObjectOfType<playerInteraction>();
    }

    private void Update()
    {
        itemLookedAt = viewForward();
        //
        if(itemLookedAt == null) 
        {
            Debug.Log("Looking at Nothing");
            if(messageDisplayed == true)
            {
                removeToolTip(currentMessage);
            }
            return;
        }
        else
        {
            Debug.Log("Looking at Something");

            if(itemLookedAt.tag == "pickUpItem")
            {
                if(messageDisplayed == false)
                {
                    toolTipLoc = GetToolTipLocation(itemLookedAt);
                    createToolTip(itemLookedAt);
                }
            }
        }
/*        toolTipLoc = GetToolTipLocation(itemLookedAt);
        // Item is Null
        if (itemLookedAt == null) 
        {
            // There is already a message being displayed, take it out.
            if(messageDisplayed == true)
            {
                removeToolTip(currentMessage);
                Debug.Log("Not seeing anything");
                return;
            }
        }
        //The Item viewed is not null;
        else if (itemLookedAt != null)
        {
            //if there is already a message showing
            if(messageDisplayed == false)
            {
                createToolTip(itemLookedAt);
            }
        }
        else if (itemLookedAt.tag != "pickUpItem") { return; }*/
    }

    public GameObject viewForward()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 5f))
        {
            //print(hit.transform.name);
            if (hit.transform.gameObject != null)
            {
                if (hit.transform.tag == "Interactable")
                {
                    return hit.transform.gameObject;
                }
                else if (hit.transform.tag == "pickUpItem")
                {
                    return hit.transform.gameObject;
                }
            }
        }
        return null;
    }

    private Transform GetToolTipLocation(GameObject parent)
    {
        foreach(Transform child in parent.transform)
        {
            if (child.tag == "ToolTipLoc")
            {
                return child;
            }
        }
        return null;
    }

    private void createToolTip(GameObject item)
    {
        if(item.tag == "pickUpItem")
        {
            if(item.GetComponent<PickUpItem>().itemType == ItemType.CannonBall)
            {
                currentMessage = Instantiate(cannonToolTip, toolTipLoc.transform.position, Quaternion.identity);
            }
            else if (item.GetComponent<PickUpItem>().itemType == ItemType.Banana)
            {
                currentMessage = Instantiate(bananaToolTip, toolTipLoc.transform.position, Quaternion.identity);
            }
            else if (item.GetComponent<PickUpItem>().itemType == ItemType.Coconut)
            {
                currentMessage = Instantiate(coconutToolTip, toolTipLoc.transform.position, Quaternion.identity);
            }
            messageDisplayed = true;
            //currentMessage = item.gameObject;
        }
    }

    private void removeToolTip(GameObject item)
    {
        Destroy(item.gameObject);
        messageDisplayed = false;
        currentMessage = null;
    }
}
