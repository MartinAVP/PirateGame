using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        this.transform.localScale = Vector3.one * 1.05f;
        this.transform.GetChild(1).gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.transform.localScale = Vector3.one;
        this.transform.GetChild(1).gameObject.SetActive(false);
    }
}
