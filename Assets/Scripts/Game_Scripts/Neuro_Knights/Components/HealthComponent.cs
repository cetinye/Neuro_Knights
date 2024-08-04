using DG.Tweening;
using UnityEngine;

namespace Neuro_Knights
{
	public class HealthComponent : MonoBehaviour
	{
		public float health;
		public float maxHealth;
		public HealthBar healthBar;

		public void Initialize(float maxHealth)
		{
			this.maxHealth = maxHealth;
			health = maxHealth;
			healthBar.SetHealth(health);
		}

		public void TakeDamage(float damage)
		{
			health -= damage;
			health = Mathf.Clamp(health, 0f, maxHealth);
			healthBar.SetHealth(health / maxHealth);

			DamagePopup.Create(transform.position, damage);

			if (health == 0)
			{
				TryGetComponent(out Target target);
				target.enabled = false;

				TryGetComponent(out AnimationComponent animationComponent);
				animationComponent.walkSeq.Kill();

				TryGetComponent(out EffectComponent effectComponent);
				effectComponent.PlayBloodParticle();

				TryGetComponent(out Enemy enemy);
				enemy.SpawnXP();

				AudioManager.instance.PlayOneShot(SoundType.Death);
				LevelManager.instance.GetEnemySpawner().RemoveEnemy(enemy);

				Destroy(gameObject);
			}
		}

		public bool IsDead()
		{
			return health <= 0;
		}
	}
}
