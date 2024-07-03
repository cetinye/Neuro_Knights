using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Neuro_Knights
{
	public class Enemy : MonoBehaviour
	{
		[SerializeField] private float speed;
		[SerializeField] private float distanceToPlayer;
		[SerializeField] private float health;
		[SerializeField] private float maxHealth;
		[SerializeField] private HealthBar healthBar;

		private LevelManager levelManager;
		private Player player;
		private SpriteRenderer spriteRenderer;
		private bool isFollowing = false;

		void Awake()
		{
			spriteRenderer = GetComponent<SpriteRenderer>();

			health = maxHealth;
			healthBar.SetHealth(health);
		}

		void Start()
		{
			levelManager = LevelManager.instance;
			player = levelManager.GetPlayer();
			isFollowing = true;
		}

		void Update()
		{
			if (isFollowing && health > 0)
			{
				FollowPlayer();
				distanceToPlayer = GetDistanceToPlayer();
			}
		}

		private void FollowPlayer()
		{
			// transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
			transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, player.transform.position.y, -0.004f), speed * Time.deltaTime);
			LookAtPlayer();
		}

		private void LookAtPlayer()
		{
			Vector2 targetPos;

			targetPos.x = player.transform.position.x - transform.position.x;
			targetPos.y = player.transform.position.y - transform.position.y;
			float angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
		}

		public float GetDistanceToPlayer()
		{
			return Vector2.Distance(transform.position, player.transform.position);
		}

		public void TakeDamage(float damage)
		{
			health -= damage;
			health = Mathf.Clamp(health, 0f, maxHealth);

			PlayBloodParticle();
			healthBar.SetHealth(health / maxHealth);

			if (health == 0)
			{
				levelManager.GetEnemySpawner().RemoveEnemy(this);
				Destroy(gameObject);
			}
		}

		private void PlayBloodParticle()
		{
			// ParticleSystem particleSystem = Instantiate(bloodParticle, transform.position, Quaternion.identity, transform);

			Sequence sequence = DOTween.Sequence();
			sequence.Append(spriteRenderer.DOColor(Color.red, 0.1f));
			sequence.Append(spriteRenderer.DOColor(Color.white, 0.1f));
		}
	}
}
