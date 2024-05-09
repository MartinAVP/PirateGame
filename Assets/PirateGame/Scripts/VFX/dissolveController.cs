using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;

public class dissolveController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Material mat;
    void Start()
    {

    }

    // Update is called once per frame
    public float lerpedValue;
    float lerpDuration = 2f;
    public float currentLerpTime = 0f;
    void Update()
    {

        currentLerpTime += Time.deltaTime;

        float t = Mathf.Clamp01(currentLerpTime / lerpDuration);

        lerpedValue = Mathf.Lerp(1, -1, t);

        mat.SetFloat("_Effect", lerpedValue);

        if(currentLerpTime >= lerpDuration)
        {
            currentLerpTime = 0;
        }
    }

    private void OnDisable()
    {
        mat.SetFloat("_Effect", -1);
    }
}
