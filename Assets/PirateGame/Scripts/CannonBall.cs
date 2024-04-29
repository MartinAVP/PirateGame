using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    [SerializeField][Range(0.1f, 100)] private float maxLifeTime = 60;
    [HideInInspector] public CannonHitRegister cannonHit;

    private void Start()
    {
        StartCoroutine(lifetime());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "CannonTarget")
        {
            cannonHit.cannonBallHitTarget();
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
