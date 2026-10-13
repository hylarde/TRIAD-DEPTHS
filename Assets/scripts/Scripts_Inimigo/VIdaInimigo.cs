using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VIdaInimigo : MonoBehaviour
{
   [Header("Vida")]
    [SerializeField] private int maxHealth = 3;

    [Header("Reação ao dano")]
    [SerializeField] private float knockbackDuration = 0.12f;
    [SerializeField] private Rigidbody2D body;

    private int currentHealth;
    private float knockbackTimer;
    private Vector2 knockbackVelocity;
    private bool isInvulnerable;

    public bool IsAlive => currentHealth > 0;
    public int CurrentHealth => currentHealth;
    public bool IsKnockedBack => knockbackTimer > 0f;

    private void Awake()
    {
        currentHealth = maxHealth;

        if (body == null)
            body = GetComponent<Rigidbody2D>();

        if (body == null)
        {
            Debug.LogWarning($"{name}: Health recomenda um Rigidbody2D para knockback.", this);
        }
    }

    private void FixedUpdate()
    {
        if (knockbackTimer <= 0f || body == null)
            return;

        body.velocity = knockbackVelocity;
        knockbackTimer -= Time.fixedDeltaTime;

        if (knockbackTimer <= 0f)
        {
            body.velocity = Vector2.zero;
            isInvulnerable = false;
        }
        else
        {
            isInvulnerable = true;
        }
    }

    public void TakeDamage(int damage, Vector2 hitPoint, Vector2 knockback)
    {
        if (!IsAlive)
            return;
        if (damage <= 0)
            return;

        if (isInvulnerable)
            return;

        currentHealth = Mathf.Max(0, currentHealth - damage);

        if (body != null && knockback.sqrMagnitude > 0.001f)
        {
            knockbackVelocity = knockback;
            knockbackTimer = knockbackDuration;
            isInvulnerable = true;
        }

        Debug.Log($"{name} recebeu {damage} de dano. Vida: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        // Primeiro teste: destrói o inimigo.
        // Depois você pode trocar por animação, drop, pontuação e respawn.
        Destroy(gameObject);
    }
}
