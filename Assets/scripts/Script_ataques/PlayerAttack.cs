using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float tempoataquedecorrido = 0f;
    public float ataqueDuracao = 0.5f;
    private Animator animator;

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
        
        animator.SetTrigger("ataque");
    }
}




