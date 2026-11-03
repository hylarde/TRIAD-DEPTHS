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

    void Update()
    {
        CoracaoLogica();
    }
    
    void CoracaoLogica()
    {

        if(vidaAtual > vidaMaxima)
        {
            vidaAtual = vidaMaxima;
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
}
