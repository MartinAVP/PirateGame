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

    private playerInteraction playerInt;
    private playerController playerCtrl;

    [SerializeField] private Transform playerLoc;
    [SerializeField] private Transform CameraLoc;
    [SerializeField] private bool hasPlayerSnapped = false;

    float cameraCap;
    float sideWaysCap;
    float mouseSensitivity = 2.8f;
    Vector2 currentMouseDelta;
    Vector2 currentMouseDeltaVelocity;
    [SerializeField][Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;

    private void Start()
    {
        playerInt = FindAnyObjectByType<playerInteraction>();
        playerCtrl = FindAnyObjectByType<playerController>();
    }

    private void FixedUpdate()
    {
        if(hasPlayerSnapped) 
        {
            UpdateMouse();
        }

        if(Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    void UpdateMouse()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        cameraCap -= currentMouseDelta.y * mouseSensitivity;
        cameraCap = Mathf.Clamp(cameraCap, -20.0f, 35.0f);

        pivot.localEulerAngles = Vector3.right * cameraCap;

        sideWaysCap -= currentMouseDelta.x * mouseSensitivity;
        sideWaysCap = Mathf.Clamp(sideWaysCap, -15f, 15f);
        //print(sideWaysCap);
        
        transform.localEulerAngles = Vector3.up * sideWaysCap;
        
    }

    private void ShootCannon()
    {

        if (hasPlayerSnapped == true)
        {
            // Player Exit From Cannon
            
            // toggle Player Snapped
            hasPlayerSnapped = false;

            // Get the player on the right rotation
            //playerCtrl.gameObject.transform.rotation = Quaternion.LookRotation(Vector3.back);
            //playerCtrl.gameObject.transform.rotation = Quaternion.LookRotation(CameraLoc.transform.forward);
            //.gameObject.transform.LookAt(CameraLoc);
            //playerInt.getCameraPos().transform.LookAt(CameraLoc);
            //playerInt.gameObject.transform.rotation.x = Quaternion.LookRotation(firePos.position).x;

            // Get the Camera to be Snapped to the Player
            Camera.main.gameObject.transform.position = playerInt.getCameraPos().position;
            Camera.main.gameObject.transform.parent = playerInt.getCameraPos();

            // Get the player Movement to lock
            playerCtrl.canMove = true;

            //Debug.Log("Player is no longer snapped to the cannon");
        }

        else if (playerInt.checkObject() == null) { return; }
        else if (playerInt.checkObject().transform.parent.gameObject == this.gameObject)
        {
            //Debug.Log("Shoot Called");
            if (!hasPlayerSnapped)
            {
                // togglePlayerSnapped
                hasPlayerSnapped = true;

                // Get the player to stand at the playerPos;
                playerCtrl.gameObject.transform.position = playerLoc.position;
                playerCtrl.gameObject.transform.rotation = Quaternion.LookRotation(Vector3.back);

                // Get the Camera to be Snapped to the Cannon
                Camera.main.gameObject.transform.position = CameraLoc.transform.position;
                Camera.main.gameObject.transform.parent = CameraLoc.transform;
                Camera.main.transform.rotation = new Quaternion(0,0,0,0);

                // Get the player Movement to lock
                playerCtrl.canMove = false;

                //Debug.Log("Player is now snapped to the cannon");
            }
/*            else
            {
                // toggle Player Snapped
                hasPlayerSnapped = false;

                // Get the Camera to be Snapped to the Player
                Camera.main.gameObject.transform.position = playerInt.getCameraPos().position;
                Camera.main.gameObject.transform.parent = playerInt.getCameraPos();

                // Get the player Movement to lock
                playerCtrl.canMove = true;

                //Debug.Log("Player is no longer snapped to the cannon");
            }*/
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
