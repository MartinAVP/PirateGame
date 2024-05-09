using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleLifeTime : MonoBehaviour
{
    public int lifetime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Lifetime());
    }

    private IEnumerator Lifetime()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(this.gameObject);
    }
}
