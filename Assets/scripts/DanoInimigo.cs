using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanoInimigo : MonoBehaviour
{
    public CoracaoSystema heart;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        CoracaoSystema vidaJogador = heart;
        if (vidaJogador == null)
        {
            vidaJogador = collision.gameObject.GetComponentInParent<CoracaoSystema>();
        }

        if (vidaJogador != null)
        {
            vidaJogador.vidaAtual--;
        }
    }
}
