using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Neuro_Knights
{
	public class Enemy : MonoBehaviour
	{
		public float speed;
		public float distanceToPlayer;
		public float health;
		public float maxHealth;
		public HealthBar healthBar;
		public Target target;

		public LevelManager levelManager;
		public Player player;
		public SpriteRenderer spriteRenderer;
		public bool isFollowing = false;

		[SerializeField] private XP xP;

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

		public virtual void FollowPlayer()
		{

		}

		public void LookAtPlayer()
		{
			Vector2 targetPos;

			targetPos.x = player.transform.position.x - transform.position.x;
			targetPos.y = player.transform.position.y - transform.position.y;
			float angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
			// transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

			if (targetPos.x > 0)
			{
				transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
			}

			else if (targetPos.x < 0)
			{
				transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
			}
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
				target.enabled = false;
				AudioManager.instance.PlayOneShot(SoundType.Death);
				levelManager.GetEnemySpawner().RemoveEnemy(this);
				SpawnXP();
				Destroy(gameObject);
			}
		}

		public void PlayBloodParticle()
		{
			// ParticleSystem particleSystem = Instantiate(bloodParticle, transform.position, Quaternion.identity, transform);

			Sequence sequence = DOTween.Sequence();
			sequence.Append(spriteRenderer.DOColor(Color.red, 0.1f));
			sequence.Append(spriteRenderer.DOColor(Color.white, 0.1f));
		}

		private void SpawnXP()
		{
			Quaternion quaternion = Quaternion.identity;
			quaternion.eulerAngles = new Vector3(0f, 0f, UnityEngine.Random.Range(0, 180));

			Instantiate(xP, transform.position, quaternion);
		}
	}
}
