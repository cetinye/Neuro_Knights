using UnityEngine;
using DG.Tweening;

namespace Neuro_Knights
{
	public class Lifebox : MonoBehaviour
	{
		[SerializeField] private float healAmount;
		[SerializeField] private GameObject lifeboxGlowParticle;
		[SerializeField] private float fadeOutTime;
		private SpriteRenderer spriteRenderer;
		private Tween fadeOut;

		void Awake()
		{
			spriteRenderer = GetComponent<SpriteRenderer>();

			lifeboxGlowParticle.SetActive(true);
		}

		void OnTriggerEnter2D(Collider2D other)
		{
			if (other.gameObject.TryGetComponent(out Player player))
			{
				player.Heal(healAmount);
				Disappear();
			}
		}

		private void Disappear()
		{
			if (fadeOut != null) return;

			lifeboxGlowParticle.SetActive(false);
			fadeOut = spriteRenderer.DOFade(0, fadeOutTime).OnComplete(() => Destroy(gameObject));
		}
	}
}
