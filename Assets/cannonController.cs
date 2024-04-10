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

    private void Start()
    {
        
    }

    private void ShootCannon()
    {
        GameObject shotFired = null;
        shotFired = Instantiate(cannonProyectile, firePos.transform.position, Quaternion.identity);

        shotFired.GetComponent<Rigidbody>().AddForce(Vector3.fwd, ForceMode.Impulse);
    }
}
