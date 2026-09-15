using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoJogador : MonoBehaviour { 
[Header("Movimento")]
    public float moveSpeed = 5f;
private Vector2 movement;
private Rigidbody2D rb;

[Header("Dash")]
public float dashSpeed = 15f;
public float dashDuration = 0.2f;
public float dashCooldown = 1f;

private bool isDashing;
private float dashTimeLeft;
private float lastDashTime = -100f;

void Start()
{
    rb = GetComponent<Rigidbody2D>();

    // Configurações padrão para top-down 2D
    rb.gravityScale = 0f;
    rb.freezeRotation = true;
}

void Update()
{
    // Se o personagem estiver dando dash, ignoramos novos comandos de movimento
    if (isDashing) return;

    // Recebe os comandos (1, -1 ou 0)
    movement.x = Input.GetAxisRaw("Horizontal");
    movement.y = Input.GetAxisRaw("Vertical");

    // Normaliza o vetor para que ele não ande mais rápido nas diagonais
    movement = movement.normalized;

    // Comando para o Dash (Tecla de Espaço)
    if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time >= lastDashTime + dashCooldown)
    {
        if (movement != Vector2.zero) // Só dá dash se tiver uma direção escolhida
        {
            StartDash();
        }
    }
}

void FixedUpdate()
{
    if (isDashing)
    {
        // Aplica a velocidade alta do dash
        rb.velocity = movement * dashSpeed;

        dashTimeLeft -= Time.fixedDeltaTime;
        if (dashTimeLeft <= 0f)
        {
            isDashing = false;
        }
    }
    else
    {
        // Aplica a velocidade de caminhada normal
        rb.velocity = movement * moveSpeed;
    }
}

private void StartDash()
{
    isDashing = true;
    dashTimeLeft = dashDuration;
    lastDashTime = Time.time;
}
}
