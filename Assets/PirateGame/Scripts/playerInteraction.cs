using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerInteraction : MonoBehaviour
{
    [SerializeField]private Transform CameraPos;
    [SerializeField]private Transform CameraLoc;
    [SerializeField]private Transform handItemPos;
    [SerializeField]private bool canInteract = true;
    [SerializeField][Range(0.1f, 1)] private float interactDelay = .1f;

    private UIManager ui;
    private playerController controller;
    private InventoryManager inventory;
    private GameItems gameItems;

    private bool hasItemInHand = false;

    private bool usingCannon;
    private void Start()
    {
        CameraPos = Camera.main.transform;
        ui = FindObjectOfType<UIManager>();
        controller = FindObjectOfType<playerController>();
        inventory = FindObjectOfType<InventoryManager>();
        gameItems = FindObjectOfType<GameItems>();
    }

    public Transform getCameraPos(){ return CameraLoc; }

/*    private void OnGUI()
    {
        if (GUILayout.Button("Banana On Hand"))
            handItem(ItemType.Banana);
        if (GUILayout.Button("Cannon Ball On Hand"))
            handItem(ItemType.CannonBall);
    }*/


    private ItemType currentItemInHand;
    GameObject itemHeld = null;
    public void handItem(ItemType type)
    {
        // Player is not Holding an Item
        if (hasItemInHand == false)
        {
            itemHeld = Instantiate(gameItems.GetPrefab(type), handItemPos.position, handItemPos.transform.rotation);
            itemHeld.transform.parent = CameraPos;

            currentItemInHand = type;
            hasItemInHand = true;
        }
        // Player is Holding an Item
        else if (hasItemInHand == true)
        {
            // Item is the same as the one already being held
            if(type == currentItemInHand)
            {
                return;
            }
            // Item is a different Item than the one already in hand
            else
            {
                // The Player clicked on an empty Slot
                if(type == ItemType.None) { Destroy(itemHeld); return; }

                Destroy(itemHeld);
                itemHeld = Instantiate(gameItems.GetPrefab(type), handItemPos.position, handItemPos.transform.rotation);
                itemHeld.transform.parent = CameraPos;
                currentItemInHand = type;
            }
        }
    }

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

    public void InventoryAccesing(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //print("E is performed");
            controller.updateMouse = false;
            ui.openInventory();
        }
        else if (context.canceled)
        {
            //print("E no longer");
            controller.updateMouse = true;
            ui.closeInventory();
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
                if (hit.transform.tag == "Interactable")
                {
                    return hit.transform.gameObject;
                }
                else if (hit.transform.tag == "pickUpItem")
                {
                    return hit.transform.gameObject;
                }
                else
                {
                    return null;
                }
            }
            else if(hit.transform.gameObject == null) { return null; }
            else { return null; }
        }

        return null;
    }

/*    private void OnGUI()
    {
        if (GUILayout.Button("Check Object"))
            checkObject();
    }*/
}
