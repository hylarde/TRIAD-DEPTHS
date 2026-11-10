using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform pontodeataquedireita;
    public Transform pontodeataqueesquerda;
    public float raioataque;

    public float tempoataquedecorrido = 0f;
    public float ataqueDuracao = 0.5f;
    private Animator animator;

    public MovimentoJogador jogador;
    void Start()
    {
        
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        tempoataquedecorrido -= Time.deltaTime;
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (tempoataquedecorrido <= 0)
            {
                Atacar();
                tempoataquedecorrido = ataqueDuracao;
            }
        }
    }

    void Atacar()
    {
        Transform pontodeataque;
        if (this.jogador.direcaoMovimento == direcaomovimento.direita)
        {
            pontodeataque = this.pontodeataquedireita;
        } else
            {
            pontodeataque = this.pontodeataqueesquerda;
        }

        Collider2D colliderinimigo = Physics2D.OverlapCircle(pontodeataque.position, this.raioataque);
        if (colliderinimigo != null)
        {
            Debug.Log("Inimigo atingido: ");
            VIdaInimigo inimigo = colliderinimigo.GetComponent<VIdaInimigo>();
            if (inimigo != null)
            {
                inimigo.TakeDamage();
            }
        }
        animator.SetTrigger("ataque");
    }
    private void OnDrawGizmos()
    {
        if (pontodeataquedireita != null)                           
        {
            Gizmos.DrawWireSphere(this.pontodeataquedireita.position, raioataque);
        }
        if (pontodeataqueesquerda != null)
        {
            Gizmos.DrawWireSphere(this.pontodeataqueesquerda.position, raioataque);
        }
    }

}




