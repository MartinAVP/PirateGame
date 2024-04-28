using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BarrelInteractionManager : MonoBehaviour
{
    #region eventBus
    private void OnEnable()
    {
        GameEventBus.Subscribe(GameEventsType.INTERACT, AccessBarrel);
    }
    private void OnDisable()
    {
        GameEventBus.Unsubscribe(GameEventsType.INTERACT, AccessBarrel);
    }
    #endregion

    private bool playerUsingBarrel = false;

    private playerController playerCtrl;
    private playerInteraction playerInt;
    private ContainerUIContainerInventory containerUI;
    private PlayerUIManager playerUI;

    private void Awake()
    {
        playerCtrl = FindFirstObjectByType<playerController>();
        playerInt = FindFirstObjectByType<playerInteraction>();
        containerUI = GetComponent<ContainerUIContainerInventory>();

        playerUI = FindFirstObjectByType<PlayerUIManager>();

    }

    private void AccessBarrel()
    {
        if (playerUsingBarrel == true)
        {
            // toggle Player Snapped
            playerUsingBarrel = false;

            // Get the player Movement to lock
            playerCtrl.canMove = true;
            playerCtrl.updateMouse = true;

            // CloseBarrel UI
            containerUI.CloseBarrel();
            playerUI.closeContainerInventory();

            //Debug.Log("Player is no longer snapped to the cannon");
        }

        else if (playerInt.checkObject() == null) { return; }
        else if (playerInt.checkObject().tag != "Interactable") { return; }
        else if (playerInt.checkObject().transform.parent.gameObject == this.gameObject)
        {
            //Debug.Log("Shoot Called");
            if (!playerUsingBarrel)
            {
                // togglePlayerSnapped
                playerUsingBarrel = true;

                // Get the player Movement to lock
                playerCtrl.canMove = false;
                playerCtrl.updateMouse = false;

                // OpenBarrelUI;
                containerUI.OpenBarrel();
                playerUI.openContainerInventory();

            }
        }
    }
}
