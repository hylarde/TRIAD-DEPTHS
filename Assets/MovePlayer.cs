using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovePlayer : MonoBehaviour
{

    [Header("Movimento")]
    [SerializeField] private float moveSpeed = 4f;

    [Header("Animação opcional")]
    [SerializeField] private Animator animator;

    private Rigidbody2D rb;
    private Vector2 input;
    private Vector2 lastDirection = Vector2.down;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
    }

    private void Update()
    {
        // Input.GetAxisRaw retorna -1, 0 ou 1 para cada eixo.
        input = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );

        // Impede que a diagonal fique mais rápida que os outros sentidos.
        if (input.sqrMagnitude > 1f)
        {
            input.Normalize();
        }

        if (input.sqrMagnitude > 0.01f)
        {
            lastDirection = input.normalized;
        }

        AtualizarAnimacao();
    }

    private void FixedUpdate()
    {
        Vector2 nextPosition = rb.position + input * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(nextPosition);
    }

    private void AtualizarAnimacao()
    {
        if (animator == null)
        {
            return;
        }

        animator.SetFloat("MoveX", lastDirection.x);
        animator.SetFloat("MoveY", lastDirection.y);
        animator.SetFloat("Speed", input.sqrMagnitude);
        animator.SetBool("IsMoving", input.sqrMagnitude > 0.01f);
    }
}
