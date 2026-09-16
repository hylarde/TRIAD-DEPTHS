using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovdoButon : MonoBehaviour
{
  public float escalaHover = 1.2f;

    private Vector3 escalaOriginal;

    private void Start()
    {
        escalaOriginal = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = escalaOriginal * escalaHover;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = escalaOriginal;
    }
}
 
