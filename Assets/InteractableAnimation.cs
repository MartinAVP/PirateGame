using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class InteractableAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private playerInteraction playerInt;
    [SerializeField] private string animBoolName;

    #region eventBus
    private void OnEnable()
    {
        GameEventBus.Subscribe(GameEventsType.INTERACT, Interact);
    }
    private void OnDisable()
    {
        GameEventBus.Unsubscribe(GameEventsType.INTERACT, Interact);
    }
    #endregion

    private void Start()
    {
        playerInt = GameObject.FindAnyObjectByType<playerInteraction>();

    }

    private bool isInteracted = false;
    private void Interact()
    {
        if(playerInt.checkObject() == null) { return; }
        if(playerInt.checkObject() == this.gameObject || playerInt.checkObject() == this.transform.parent.gameObject)
        {
            isInteracted = !isInteracted;

            if (isInteracted)
            {
                animator.SetBool(animBoolName, true);
            }
            else
            {
                animator.SetBool(animBoolName, false);
            }
        }
        else
        {
            print("Player tried interacting with air");
        }
    }
}
