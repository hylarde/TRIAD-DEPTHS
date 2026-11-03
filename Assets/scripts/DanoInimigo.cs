using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanoInimigo : MonoBehaviour
{
    public CoracaoSystema heart;
   private void oncollisionEnter2D(Collision2D collision)
   {
       if(collision.gameObject.tag == "Player")
       {
           heart.vidaAtual--;
       }
   }
}
