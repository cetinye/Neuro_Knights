using UnityEngine;

namespace Neuro_Knights
{
	public class EffectCircle : MonoBehaviour
	{
		public float rotationSpeed;
		public int damage;
		public float burnDuration;
		public float burnInterval;

		void Update()
		{
			transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
		}

		void OnTriggerEnter2D(Collider2D other)
		{
			if (other.TryGetComponent(out Enemy enemy))
			{
				enemy.PlayBurnParticle(damage, burnDuration, burnInterval);
			}
		}
	}
}
