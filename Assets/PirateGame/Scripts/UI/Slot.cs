using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool mouse_over = false;

    void Update()
    {
        if (mouse_over)
        {
            //Debug.Log("Mouse Over");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouse_over = true;
        this.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        //Debug.Log("Mouse enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouse_over = false;
        this.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
        //Debug.Log("Mouse exit");
    }
}
