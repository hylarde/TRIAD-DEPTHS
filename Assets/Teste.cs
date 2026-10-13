using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teste : MonoBehaviour
{
   int vida = 100;

   void AplaicaDano( int dano) 
   {
        vida -= dano; 
        if (vida >= 0)
        {
            Debug.Log("Personagem morto");
        }
   }

   void Update (){
   
    if (Input.GetKeyDown(KeyCode.Space))
        {
            
            if (vida > 0)
             {

             int danoAleatorio = Random.Range(1,11);

             AplaicaDano(danoAleatorio);
             
                Debug.Log("Você sofreu " + danoAleatorio + " de dano! Vida restante: " + vida);
            }
            else
            {
                Debug.Log("O objeto já está sem vida!");
            }
            
         }
   }

   public void AplicarDano(int dano)
    {
        vida -= dano;

       
        if (vida < 0)
        {
            vida = 0;
        }
    }
}
