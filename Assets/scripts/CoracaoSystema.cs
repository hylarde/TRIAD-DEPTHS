using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CoracaoSystema : MonoBehaviour
{
   public int vidaMaxima;
   public int vidaAtual;
   public Image[] coracoes;
   public Sprite coracaoCheio;
    public Sprite coracaoVazio;
    private bool morto;

    void Update()
    {
        CoracaoLogica();
        EstadoMorto();
    }
    
    void CoracaoLogica()
    {

        if(vidaAtual > vidaMaxima)
        {
            vidaAtual = vidaMaxima;
        }
        if(vidaAtual < 0)
        {
            vidaAtual = 0;
        }
        for(int i = 0; i < coracoes.Length; i++)
        {
            if(i <vidaAtual)
            {
                coracoes[i].sprite = coracaoCheio;
            }
            else
            {
                coracoes[i].sprite = coracaoVazio;
            }
            if(i < vidaMaxima)
            {
               coracoes[i].enabled = true; 
            }
            else
            {
                coracoes[i].enabled = false;
            }
        }

    }
void EstadoMorto(){
    if(vidaAtual <= 0 && !morto)
    {
       morto = true;
       vidaAtual = 0;

       MovimentoJogador movimento = GetComponent<MovimentoJogador>();
       if(movimento != null)
       {
           movimento.enabled = false;
       }

       Rigidbody2D corpo = GetComponent<Rigidbody2D>();
       if(corpo != null)
       {
           corpo.velocity = Vector2.zero;
       }

       Destroy(gameObject, 0.5f);
    }
}

}
