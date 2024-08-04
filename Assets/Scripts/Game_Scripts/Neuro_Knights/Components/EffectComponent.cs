using DG.Tweening;
using UnityEngine;

namespace Neuro_Knights
{
	public class EffectComponent : MonoBehaviour
	{
		public ParticleSystem blood;
		public ParticleSystem burn;
		public GameObject ice;
		public bool isPoisoned = false;
		public bool isFrozen = false;
		private float initialSpeed;
		private float speedBackup;
		private float iceInitialScale;
		private float iceScaleDuration;
		private float iceMeltDuration;
		private int burnDamage = 0;
		private IMovement movementComponent;
		private HealthComponent healthComponent;
		private AnimationComponent animationComponent;

		public void Initialize(HealthComponent healthComponent, AnimationComponent animationComponent, ParticleSystem blood, ParticleSystem burn, GameObject ice, float iceScaleDuration, float iceMeltDuration)
		{
			TryGetComponent<IMovement>(out IMovement movementComponent);
			this.movementComponent = movementComponent;
			this.healthComponent = healthComponent;
			this.animationComponent = animationComponent;

			initialSpeed = movementComponent.speed;
			this.blood = blood;
			this.burn = burn;
			this.ice = ice;
			this.iceScaleDuration = iceScaleDuration;
			this.iceMeltDuration = iceMeltDuration;
			iceInitialScale = ice.transform.localScale.x;
		}

		#region Poison

		public void Poisoned(float slowPower, float duration)
		{
			if (isPoisoned) return;

			isPoisoned = true;
			speedBackup = movementComponent.speed;
			movementComponent.speed -= (movementComponent.speed * slowPower) / 100;
			Invoke(nameof(DisablePoison), duration);
		}

		private void DisablePoison()
		{
			isPoisoned = false;
			movementComponent.speed = speedBackup;
		}

		#endregion

		#region ICE

		public void PlayIceParticle(int iceDamage, float iceDuration)
		{
			if (ice.activeSelf) return;

			isFrozen = true;
			ice.transform.localScale = Vector3.zero;
			ice.SetActive(true);
			ice.transform.DOScale(iceInitialScale, iceScaleDuration).SetEase(Ease.InOutCubic);
			healthComponent.TakeDamage(iceDamage);
			initialSpeed = movementComponent.speed;
			movementComponent.speed = 0;
			animationComponent.walkSeq.Kill(true);
			Invoke(nameof(DisableIceParticle), iceDuration);
		}

		public void DisableIceParticle()
		{
			ice.transform.DOScale(0, iceMeltDuration).SetEase(Ease.Linear).OnComplete(() =>
			{
				ice.SetActive(false);
				isFrozen = false;
				movementComponent.speed = initialSpeed;
				animationComponent.WalkAnim();
			});
		}

		#endregion

		#region BURN

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

		public void TakeFireDamage()
		{
			healthComponent.TakeDamage(burnDamage);
		}

		#endregion

		public void PlayBloodParticle()
		{
			ParticleSystem spawnedParticle = Instantiate(blood, transform.position, Quaternion.identity);
			spawnedParticle.gameObject.SetActive(true);
		}
	}
}
