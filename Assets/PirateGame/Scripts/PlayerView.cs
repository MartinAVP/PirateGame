using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{

    private playerInteraction playerInt;
    private GameAssets gameAssets;

    private GameObject itemLookedAt;
    private Transform toolTipLoc;

    public GameObject currentMessage;

    private bool messageDisplayed = false;

    private void Start()
    {
        playerInt = FindObjectOfType<playerInteraction>();
        gameAssets = FindObjectOfType<GameAssets>();
    }

    private void FixedUpdate()
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
                    //Debug.Log(itemLookedAt.transform.parent.GetComponent<InteractableItem>().InteractType.ToString());
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
        if (item.tag == "pickUpItem")
        {
            spawnPickupToolTip(item.GetComponent<PickUpItem>().itemType);
        }
        else if(item.tag == "Interactable")
        {
            spawnInteractableToolTip(item.GetComponent<InteractableItem>().InteractType);
        }

        messageDisplayed = true;
    }

    private void spawnPickupToolTip(ItemType type)
    {
        currentMessage = Instantiate(gameAssets.FindItemTypeData(type).toolTip, toolTipLoc.transform.position, Quaternion.identity);
    }

    private void spawnInteractableToolTip(InteractType type)
    {
        currentMessage = Instantiate(gameAssets.FindInteractableTypeData(type).toolTip, toolTipLoc.transform.position, Quaternion.identity);
    }

    private void removeToolTip(GameObject item)
    {
        Destroy(item.gameObject);
        messageDisplayed = false;
        currentMessage = null;
    }

}
