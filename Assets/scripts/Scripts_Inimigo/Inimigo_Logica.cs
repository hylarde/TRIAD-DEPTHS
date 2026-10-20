using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo_Logica : MonoBehaviour

{

    [SerializeField]
    private LayerMask layerAreaVisao;

    [SerializeField]
    private float raioVisao;

    private Transform alvo;

    [SerializeField]
    private float velocidadeMovimento;

    [SerializeField]
    private float distanciaMinima;

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private SpriteRenderer spriteRenderer;
        
         
      private void Update()
      {

        ProcurarJogador();
        if (this.alvo != null) {
            Mover();
        } else {
            PararMovimento();
        }

      }

      private void OnDrawGizmosSelected()
      {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, raioVisao);
        if(this.alvo != null) {
            Gizmos.DrawLine(transform.position, this.alvo.position);
        }
      }

      private void ProcurarJogador()
    {
        Collider2D colisor = Physics2D.OverlapCircle(this.transform.position, this.raioVisao, this.layerAreaVisao);
       if (colisor != null) 
       {
            
         //jogador está dentro do raio de visao
            Vector2 posicaoAtual = this.transform.position;
            Vector2 posicaoAlvo = colisor.transform.position;
            Vector2 direcao = posicaoAlvo - posicaoAtual; 
            direcao = direcao.normalized;

            this.alvo = colisor.transform;

             RaycastHit2D hit = Physics2D.Raycast(posicaoAtual, direcao, LayerMask.GetMask("Obstaculo"));
            if (hit != null) 
            {
                this.alvo = colisor.transform;
            }
            else {
                this.alvo = null;
            }
            
        } 
        
        else 
        {
            this.alvo = null;
        }
      }
    
    private void Mover()
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
                this.PararMovimento();
            }
        }
     }

     private void PararMovimento()
     {
        this.rb.velocity = Vector2.zero;
        this.spriteRenderer.flipX = false;
     }

}

        
