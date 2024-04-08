using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private playerInteraction playerInt;

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

    [SerializeField] private Animator animator;
    private bool isInteracted = false;
    private void Interact()
    {
        if(playerInt.checkObject() == this.gameObject || playerInt.checkObject() == this.transform.parent.gameObject)
        {
            isInteracted = !isInteracted;

            if (isInteracted)
            {
                animator.SetBool("Open", true);
            }
            else
            {
                animator.SetBool("Open", false);
            }
        }
    }
}
