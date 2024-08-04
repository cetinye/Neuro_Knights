using UnityEngine;

namespace Neuro_Knights
{
	public class Enemy : MonoBehaviour
	{
		public float maxHealth;
		public HealthBar healthBar;
		public SpriteRenderer spriteRenderer;
		public ParticleSystem blood;
		public ParticleSystem burn;
		public GameObject ice;
		public float iceScaleDuration;
		public float iceMeltDuration;
		public float speed;
		public float xScaleAmount;
		public float xScaleTime;
		public float yScaleTime;
		[SerializeField] private XP xp;

		[HideInInspector] public Player player;

		[Header("Components")]
		public HealthComponent healthComponent;
		public AnimationComponent animationComponent;
		public EffectComponent effectComponent;

		void Update()
		{
			if (!healthComponent.IsDead() && GameStateManager.GetGameState() == GameState.Playing)
			{
				FollowPlayer();
				LookAtPlayer();
			}
		}

		public void TakeDamage(float damage)
		{
			healthComponent.TakeDamage(damage);
			if (healthComponent.IsDead())
			{
				effectComponent.PlayBloodParticle();
				Destroy(gameObject);
			}
		}

		public virtual void FollowPlayer()
		{

		}

		public void LookAtPlayer()
		{
			if (effectComponent.isFrozen) return;

			Vector2 targetPos;
			targetPos.x = player.transform.position.x - transform.position.x;

			if (targetPos.x > 0)
			{
				transform.localRotation = Quaternion.Euler(new Vector3(transform.localRotation.eulerAngles.x, 0, transform.localRotation.eulerAngles.z));
			}

			else if (targetPos.x < 0)
			{
				transform.localRotation = Quaternion.Euler(new Vector3(transform.localRotation.eulerAngles.x, 180, transform.localRotation.eulerAngles.z));
			}
		}

		public void SpawnXP()
		{
			Quaternion quaternion = Quaternion.identity;
			quaternion.eulerAngles = new Vector3(0f, 0f, UnityEngine.Random.Range(0, 180));

			Instantiate(xp, transform.position, quaternion);
		}

		public float GetDistanceToPlayer()
		{
			return Vector2.Distance(transform.position, player.transform.position);
		}

		public float GetDistanceFrom(Vector3 position)
		{
			return Vector2.Distance(transform.position, position);
		}

		#region EFFECTS

		public void Poisoned(float slowPower, float duration)
		{
			effectComponent.Poisoned(slowPower, duration);
		}

		public void Freeze(int iceDamage, float iceDuration)
		{
			effectComponent.PlayIceParticle(iceDamage, iceDuration);
		}

		public void Burn(int damage, float effectDuration, float effectInterval)
		{
			effectComponent.PlayBurnParticle(damage, effectDuration, effectInterval);
		}

		#endregion
	}
}