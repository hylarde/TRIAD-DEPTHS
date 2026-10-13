using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AttackHitbox : MonoBehaviour
{
	[Header("Dano")]
	[SerializeField] private int damage = 1;

	[Header("Força de knockback")]
	[SerializeField] private float knockbackForce = 5f;

	[Header("Layer dos inimigos")]
	[SerializeField] private LayerMask enemyLayer;

	[Header("Cooldown por inimigo (segundos)")]
	[SerializeField] private float perEnemyCooldown = 0.25f;

	private Dictionary<int, float> lastHitTimeById = new Dictionary<int, float>();

	private void Reset()
	{
		var col = GetComponent<Collider2D>();
		if (col != null)
			col.isTrigger = true;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		TryHit(other);
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		// Opcional: permitir múltiplos hits enquanto a hitbox permanece
		// mas com cooldown por inimigo
		TryHit(other);
	}

	private void TryHit(Collider2D other)
	{
		if (((1 << other.gameObject.layer) & enemyLayer.value) == 0)
			return;

		var vida = other.GetComponent<VIdaInimigo>();
		if (vida == null)
			return;

		int id = other.gameObject.GetInstanceID();
		float now = Time.time;
		if (lastHitTimeById.TryGetValue(id, out float last) && now - last < perEnemyCooldown)
			return;

		if (!vida.IsAlive)
			return;

		lastHitTimeById[id] = now;

		Vector2 attackerPos = transform.root != null ? (Vector2)transform.root.position : (Vector2)transform.position;
		Vector2 direction = ((Vector2)other.transform.position - attackerPos).normalized;
		Vector2 knockback = direction * knockbackForce;

		vida.TakeDamage(damage, other.ClosestPoint(attackerPos), knockback);
	}
}

