using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySelectedAnimation : MonoBehaviour
{
    public Animator myAnimationController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myAnimationController.SetBool("Open", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myAnimationController.SetBool("Open", false);
        }
    }
}
