using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seek : MonoBehaviour
{
    public float speed;
    public Transform player;
    public float stoppingDistance;

    // Update is called once per frame
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (player == null)
            return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > stoppingDistance)
        {
            Vector2 direction = (player.position - transform.position).normalized;

            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
        }
    }
}
