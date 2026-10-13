using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo_Logica : MonoBehaviour
{
    [SerializeField] 
    private Transform alvo;

    [SerializeField]
    private float velocidadeMovimento;

    [SerializeField]
    private float distanciaMinima;

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    
    void Update()
    {
        Vector2 posicaoAlvo = this.alvo.position;
        Vector2 posicaoAtual = this.transform.position;

        float distancia = Vector2.Distance(posicaoAlvo, posicaoAtual); 
        if (distancia >= this.distanciaMinima) {
        Vector2 direcao = posicaoAlvo - posicaoAtual;
        direcao = direcao.normalized;
            
      this.rb.velocity = (this.velocidadeMovimento * direcao);
        
        
       
        
        this.rb.velocity = (this.velocidadeMovimento * direcao);

        if (this.rb.velocity.x > 0 ) {

            this.spriteRenderer.flipX = false;
        }
        else if (this.rb.velocity.x < 0 ) {
            this.spriteRenderer.flipX = true;
            } else {
                this.rb.velocity = Vector2.zero;
            }
        }
     }
}
