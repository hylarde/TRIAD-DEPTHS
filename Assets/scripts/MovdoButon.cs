using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovdoButon : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler
{
  [SerializeField] private float altura = 10f;
    [SerializeField] private float velocidade = 80f;

    private RectTransform rectTransform;
    private Vector2 posicaoOriginal;
    private Vector2 posicaoAlvo;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        posicaoOriginal = rectTransform.anchoredPosition;
        posicaoAlvo = posicaoOriginal;
    }

    private void Update()
    {
        rectTransform.anchoredPosition = Vector2.MoveTowards(
            rectTransform.anchoredPosition,
            posicaoAlvo,
            velocidade * Time.deltaTime
        );
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        posicaoAlvo = posicaoOriginal + Vector2.up * altura;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        posicaoAlvo = posicaoOriginal;
    }
}
