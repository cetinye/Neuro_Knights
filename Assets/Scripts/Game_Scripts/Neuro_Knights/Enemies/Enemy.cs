using System;
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
		public float instantHealth; // health calculated before bullet hits
		public float maxHealth;
		public HealthBar healthBar;
		public Target target;
		public ParticleSystem blood;
		public ParticleSystem burn;

		public LevelManager levelManager;
		public Player player;
		public SpriteRenderer spriteRenderer;
		public bool isFollowing = false;
		public bool isPoisoned = false;

		[SerializeField] private XP xP;

		private int burnDamage = 0;

		[Header("Walk Anim Variables")]
		[SerializeField] private float xScaleAmount;
		[SerializeField] private float yScaleAmount;
		[SerializeField] private float xScaleTime;
		[SerializeField] private float yScaleTime;
		private Sequence walkSeq;

		void Awake()
		{
			health = maxHealth;
			instantHealth = health;
			healthBar.SetHealth(health);
		}

		public virtual void Start()
		{
			levelManager = LevelManager.instance;
			player = levelManager.GetPlayer();
			isFollowing = true;

			WalkAnim();
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
				// transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
				transform.localRotation = Quaternion.Euler(new Vector3(transform.localRotation.eulerAngles.x, 0, transform.localRotation.eulerAngles.z));
			}

			else if (targetPos.x < 0)
			{
				// transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
				transform.localRotation = Quaternion.Euler(new Vector3(transform.localRotation.eulerAngles.x, 180, transform.localRotation.eulerAngles.z));
			}
		}

		public float GetDistanceToPlayer()
		{
			return Vector2.Distance(transform.position, player.transform.position);
		}

		public float GetDistanceFrom(Vector3 position)
		{
			return Vector2.Distance(transform.position, position);
		}

		public void TakeDamage(float damage)
		{
			if (health <= 0) return;

			health -= damage;
			health = Mathf.Clamp(health, 0f, maxHealth);

			healthBar.SetHealth(health / maxHealth);

			DamagePopup.Create(transform.position, damage);

			if (health == 0)
			{
				target.enabled = false;
				walkSeq.Kill();
				AudioManager.instance.PlayOneShot(SoundType.Death);
				levelManager.GetEnemySpawner().RemoveEnemy(this);
				PlayBloodParticle();
				SpawnXP();
				Destroy(gameObject);
			}
		}

		public void InstantHealthDamage(float damage)
		{
			instantHealth -= damage;
		}

		public float GetInstantHealth()
		{
			return instantHealth;
		}

		public void TakeFireDamage()
		{
			TakeDamage(burnDamage);
		}

		public void PlayBloodParticle()
		{
			ParticleSystem spawnedParticle = Instantiate(blood, transform.position, Quaternion.identity);
			spawnedParticle.gameObject.SetActive(true);

			// Sequence sequence = DOTween.Sequence();
			// sequence.Append(spriteRenderer.DOColor(Color.red, 0.1f));
			// sequence.Append(spriteRenderer.DOColor(Color.white, 0.1f));
		}

		public void PlayBurnParticle(int burnDamage, float burnDuration, float burnInterval)
		{
			if (burn.gameObject.activeSelf) return;

			burn.gameObject.SetActive(true);
			this.burnDamage = burnDamage;
			InvokeRepeating(nameof(TakeFireDamage), 0.1f, burnInterval);
			Invoke(nameof(DisableBurnParticle), burnDuration);
		}

		public void DisableBurnParticle()
		{
			burn.gameObject.SetActive(false);
		}

		private void SpawnXP()
		{
			Quaternion quaternion = Quaternion.identity;
			quaternion.eulerAngles = new Vector3(0f, 0f, UnityEngine.Random.Range(0, 180));

			Instantiate(xP, transform.position, quaternion);
		}

		private void WalkAnim()
		{
			walkSeq = DOTween.Sequence();

			walkSeq.Append(spriteRenderer.transform.DOScaleX(spriteRenderer.transform.localScale.x + xScaleAmount, xScaleTime));
			walkSeq.Join(spriteRenderer.transform.DOScaleY(spriteRenderer.transform.localScale.y - yScaleAmount, yScaleTime));

			walkSeq.SetLoops(-1, LoopType.Yoyo);
		}

		#region Poison

		private float speedBackup;
		public void Poisoned(float slowPower, float duration)
		{
			if (isPoisoned) return;

			isPoisoned = true;
			speedBackup = speed;
			speed -= (speed * slowPower) / 100;
			Invoke(nameof(DisablePoison), duration);
		}

		private void DisablePoison()
		{
			isPoisoned = false;
			speed = speedBackup;
		}

		#endregion
	}
}
