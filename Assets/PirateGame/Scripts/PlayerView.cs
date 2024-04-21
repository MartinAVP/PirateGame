using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{

    private playerInteraction playerInt;
    private GameTooltips tooltip;

    private GameObject itemLookedAt;
    private Transform toolTipLoc;

    public GameObject currentMessage;

    private bool messageDisplayed = false;

    private void Start()
    {
        playerInt = FindObjectOfType<playerInteraction>();
        tooltip = FindObjectOfType<GameTooltips>();
    }

    private void Update()
    {
        itemLookedAt = viewForward();
        //
        if(itemLookedAt == null) 
        {
            //Debug.Log("Looking at Nothing");
            if(messageDisplayed == true)
            {
                removeToolTip(currentMessage);
            }
            return;
        }
        else
        {
            //Debug.Log("Looking at Something");

            if(itemLookedAt.tag == "pickUpItem")
            {
                if(messageDisplayed == false)
                {
                    toolTipLoc = GetToolTipLocation(itemLookedAt);
                    createToolTip(itemLookedAt);
                }
                else if(messageDisplayed == true)
                {
                    if(currentMessage != itemLookedAt)
                    {
                        removeToolTip(currentMessage);
                        toolTipLoc = GetToolTipLocation(itemLookedAt);
                        createToolTip(itemLookedAt);
                    }
                }
            }
            if (itemLookedAt.tag == "Interactable")
            {
                if(messageDisplayed == false)
                {
                    toolTipLoc = GetToolTipLocation(itemLookedAt.transform.parent.gameObject);
                    createToolTip(itemLookedAt.transform.parent.gameObject);
                }
            }
        }
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
                currentMessage = Instantiate(tooltip.GetItemTooltip(ItemType.CannonBall), toolTipLoc.transform.position, Quaternion.identity);
            }
            else if (item.GetComponent<PickUpItem>().itemType == ItemType.Banana)
            {
                currentMessage = Instantiate(tooltip.GetItemTooltip(ItemType.Banana), toolTipLoc.transform.position, Quaternion.identity);
            }
            else if (item.GetComponent<PickUpItem>().itemType == ItemType.Plank)
            {
                currentMessage = Instantiate(tooltip.GetItemTooltip(ItemType.Plank), toolTipLoc.transform.position, Quaternion.identity);
            }
            messageDisplayed = true;
            //currentMessage = item.gameObject;
        }
        else if(item.tag == "Interactable")
        {
            if (item.GetComponent<InteractableItem>().InteractType == InteractType.Cannon)
            {
                //Debug.Log("Cannon");
                currentMessage = Instantiate(tooltip.GetInteractTooltip(InteractType.Cannon), toolTipLoc.transform.position, Quaternion.identity);
            }
            else if (item.GetComponent<InteractableItem>().InteractType == InteractType.Barrel)
            {
                //Debug.Log("Cannon");
                currentMessage = Instantiate(tooltip.GetInteractTooltip(InteractType.Barrel), toolTipLoc.transform.position, Quaternion.identity);
            }
            messageDisplayed = true;
        }
    }

    private void removeToolTip(GameObject item)
    {
        Destroy(item.gameObject);
        messageDisplayed = false;
        currentMessage = null;
    }

}
