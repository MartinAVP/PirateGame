using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    [Header("Cannon Positions")]
    [SerializeField] private Transform pivot;
    [SerializeField] private Transform firePos;
    [SerializeField] private Transform playerLoc;
    [SerializeField] private Transform CameraLoc;

    [Header("Cannon Ball Prefab")]
    [SerializeField] private GameObject cannonProyectile;

    [Header("Cannon Settings")]
    [SerializeField] private float shootDelayTime = .3f;

    private playerInteraction playerInt;
    private playerController playerCtrl;

    private bool hasPlayerSnapped = false;

    private bool canShoot = true;

    public UnityEvent onShoot;

    // mouseSettings;
    float cameraCap;
    float sideWaysCap;
    float mouseSensitivity = 1.8f;
    Vector2 currentMouseDelta;
    Vector2 currentMouseDeltaVelocity;
    float mouseSmoothTime = 0.01f;

    private void Start()
    {
        playerInt = FindAnyObjectByType<playerInteraction>();
        playerCtrl = FindAnyObjectByType<playerController>();
    }

    private void Update()
    {
        if(hasPlayerSnapped) 
        {
            UpdateMouse();
        }

        // check if left click
        if(Input.GetMouseButtonDown(0))
        {
            // check if cannon has a player attached
            if(hasPlayerSnapped == true)
            {
                // check if the player can shoot
                if(canShoot == true)
                {
                    canShoot = false;
                    Fire();
                    StartCoroutine(shootDelay());

                }
            }
        }
    }

    // Function for the Delay of the shooting
    private IEnumerator shootDelay()
    {
        yield return new WaitForSeconds(shootDelayTime);
        canShoot = true;
    }

    void UpdateMouse()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        // Smooths the mouse movement
        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        // Gets the mouse input vertically and Damps it to a max and min value
        cameraCap += currentMouseDelta.y * mouseSensitivity;
        cameraCap = Mathf.Clamp(cameraCap, -20.0f, 35.0f);

        pivot.localEulerAngles = Vector3.right * cameraCap;

        // Gets the mouse input horizontally and Damps it to a max and min value
        sideWaysCap += currentMouseDelta.x * mouseSensitivity;
        sideWaysCap = Mathf.Clamp(sideWaysCap, -15f, 15f);
        //print(sideWaysCap);
        
        transform.localEulerAngles = Vector3.up * sideWaysCap;
        
    }

    private void ShootCannon()
    {
        if (hasPlayerSnapped == true)
        {
            // toggle Player Snapped
            hasPlayerSnapped = false;

            // Get the Camera to be Snapped to the Player
            Camera.main.gameObject.transform.position = playerInt.getCameraPos().position;
            Camera.main.gameObject.transform.parent = playerInt.getCameraPos();

            // Get the player Movement to lock
            playerCtrl.canMove = true;

            //Debug.Log("Player is no longer snapped to the cannon");
        }

        else if (playerInt.checkObject() == null) { return; }
        else if (playerInt.checkObject().tag != "Interactable") { return; }
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

            }
        }
    }

    private void Fire()
    {
        // Creates a new Cannon Ball at the firing position with the pivot transformation
        GameObject shotFired = Instantiate(cannonProyectile, firePos.transform.position, pivot.transform.rotation);

        firePos.transform.rotation = pivot.transform.rotation;

        shotFired.GetComponent<CannonBall>().cannonHit = FindObjectOfType<CannonHitRegister>();

        // adds the Force to the cannon Ball.
        shotFired.GetComponent<Rigidbody>().AddRelativeForce(Vector3.back * 50f, ForceMode.Impulse);
        shotFired.GetComponent<Rigidbody>().AddForce(Vector3.up * 2f, ForceMode.Impulse);

        // Invoke Event for Shooting
        onShoot.Invoke();
    }
}
