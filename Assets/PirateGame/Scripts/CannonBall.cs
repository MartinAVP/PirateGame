using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CannonBall : MonoBehaviour
{
    [SerializeField][Range(0.1f, 100)] private float maxLifeTime = 60;
    [SerializeField] public int damage = 50;
    [HideInInspector] public CannonHitRegister cannonHit;

    public UnityEvent hitShip;
    private void Start()
    {
        StartCoroutine(lifetime());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "CannonTarget")
        {
            cannonHit.cannonBallHitTarget();
            collision.transform.parent.parent.GetComponent<ShipManager>().takeDamage(damage);
            hitShip.Invoke();
        }
        DestroyProjectile();
    }

    private IEnumerator lifetime()
    {
        yield return new WaitForSeconds(maxLifeTime);
        DestroyProjectile();
    }

    private void DestroyProjectile()
    {
        Destroy(this.gameObject);
    }
}
