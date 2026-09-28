using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teste : MonoBehaviour
{
   int vida = 100;

   Void AplaicaDano( int dano) 
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
             
            }
         }
   }

   
}
