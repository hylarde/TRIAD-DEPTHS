using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class perseguir : MonoBehaviour
{
    public float speed;
    public Transform player;
    public float stoppingDistance;
    private VIdaInimigo vidaInimigo;
    public Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        vidaInimigo = GetComponent<VIdaInimigo>();
    }

    void FixedUpdate()
    {
        if (player == null || rb == null)
            return;

        // Se estiver em knockback, não perseguir
        if (vidaInimigo != null && vidaInimigo.IsKnockedBack)
            return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > stoppingDistance)
        {
            Vector2 direction = (player.position - transform.position).normalized;

            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
        }
    }
}
