using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannonController : MonoBehaviour
{
    #region eventBus
    private void OnEnable()
    {
        GameEventBus.Subscribe(GameEventsType.INTERACT, ShootCannon);
    }
    private void OnDisable()
    {
        GameEventBus.Unsubscribe(GameEventsType.INTERACT, ShootCannon);
    }
    #endregion

    [SerializeField] private Transform pivot;
    [SerializeField] private Transform firePos;
    [SerializeField] private GameObject cannonProyectile;

    [SerializeField] private playerInteraction playerInt;
    [SerializeField] private Transform CameraLoc;
    [SerializeField] private bool hasPlayerSnapped = false;

    private void Start()
    {
        playerInt = FindAnyObjectByType<playerInteraction>();
    }

    private void FixedUpdate()
    {
        if(Input.GetKeyUp(KeyCode.P)) 
        {
            
        }
    }

    private void ShootCannon()
    {
        if (playerInt.checkObject() == null) { return; }
        Debug.Log(playerInt.checkObject().name);
        if (playerInt.checkObject().transform.parent.gameObject == this.gameObject)
        {
            Debug.Log("Shoot Called");
            if (!hasPlayerSnapped)
            {
                // togglePlayerSnapped
                hasPlayerSnapped = true;

                // Get the Camera to be Snapped to the Cannon
                Camera.main.gameObject.transform.position = CameraLoc.transform.position;
                Camera.main.gameObject.transform.parent = CameraLoc.transform;
                Debug.Log("Player is now snapped to the cannon");
            }
            else
            {
                // toggle Player Snapped
                hasPlayerSnapped = false;

                // Get the Camera to be Snapped to the Player
                Camera.main.gameObject.transform.position = playerInt.getCameraPos().position;
                Camera.main.gameObject.transform.parent = playerInt.getCameraPos();
                Debug.Log("Player is no longer snapped to the cannon");
            }
        }
        else
        {
            print("Player tried interacting with air");
        }
    }

    private void Fire()
    {
        GameObject shotFired = null;
        shotFired = Instantiate(cannonProyectile, firePos.transform.position, Quaternion.identity);

        shotFired.GetComponent<Rigidbody>().AddForce(Vector3.back * 30f, ForceMode.Impulse);
        shotFired.GetComponent<Rigidbody>().AddForce(Vector3.up * 2f, ForceMode.Impulse);

    }
}
