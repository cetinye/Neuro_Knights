using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Neuro_Knights
{
	using UnityEngine;

	public class LightningEffectCircle : MonoBehaviour
	{
		public float damage = 2.0f;
		public float radius = 2.0f; // Radius of the circular path
		public float speed = 1.0f; // Speed of the orbit
		public float enemyJumpRange = 5.0f; // Range to check for nearby enemies
		public int maxJumps = 3; // Maximum number of jumps
		public float moveSpeed = 5.0f; // Speed of moving towards the enemy
		public ParticleSystem lightningExplosion; // Particle system to play when lightning hits enemy

		private Transform player; // Reference to the player
		private float angle; // Current angle
		private bool isMovingToEnemy = false; // Flag to check if moving to an enemy
		private bool isTriggerable = true; // Flag to check if the lightning can be triggered
		private int totalJumps = 0; // Total number of jumps
		private Transform targetEnemy; // Current target enemy

		void Start()
		{
			player = LevelManager.instance.GetPlayer().transform;
		}

		void Update()
		{
			if (GameStateManager.GetGameState() != GameState.Playing)
				return;

			if (isMovingToEnemy && targetEnemy != null)
			{
				MoveTowardsTarget();
			}
			else
			{
				CircularMotion();
			}
		}

		void CircularMotion()
		{
			if (player == null) return;

			// Increment the angle based on the speed and time
			angle += speed * Time.deltaTime;

			// Calculate the new position using sine and cosine functions
			float x = Mathf.Cos(angle) * radius;
			float y = Mathf.Sin(angle) * radius;

			// Update the particle's position
			transform.position = new Vector3(x, y, 0) + player.position;
		}

		void MoveTowardsTarget()
		{
			// Move towards the target (enemy or player)
			transform.position = Vector3.MoveTowards(transform.position, targetEnemy.position, moveSpeed * Time.deltaTime);

			// Check if reached the target
			if (Vector2.Distance(transform.position, targetEnemy.position) <= 0.1f)
			{
				if (targetEnemy == player)
				{
					// Reset jumps when returning to the player
					totalJumps = 0;
					isMovingToEnemy = false;
					isTriggerable = true;
				}
				else
				{
					DamageEnemy();

					totalJumps++;

					if (totalJumps >= maxJumps)
					{
						// If jump limit is reached, return to player
						isTriggerable = false;
						targetEnemy = player;
					}
					else
					{
						// Find next enemy
						FindNextEnemy();
					}
				}
			}
		}

		void DamageEnemy()
		{
			Instantiate(lightningExplosion, transform.position, Quaternion.identity);

			if (targetEnemy.TryGetComponent(out Enemy enemy))
				enemy.TakeDamage(damage);
		}

		void FindNextEnemy()
		{
			Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, enemyJumpRange);
			foreach (var hitCollider in hitColliders)
			{
				if (hitCollider.TryGetComponent(out Enemy enemy) && hitCollider.transform != targetEnemy)
				{
					targetEnemy = hitCollider.transform;
					return;
				}
			}

			// No more enemies to jump to, return to player
			targetEnemy = player;
		}

		void OnTriggerEnter2D(Collider2D other)
		{
			if (isTriggerable && other.TryGetComponent(out Enemy enemy))
			{
				targetEnemy = other.transform;
				isMovingToEnemy = true;
			}
		}

		// Optional: Visualize the range in the editor
		void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(transform.position, enemyJumpRange);
		}
	}
}