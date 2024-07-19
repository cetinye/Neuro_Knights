using UnityEngine;

namespace Neuro_Knights
{
	public class EffectCircle : MonoBehaviour
	{
		public float rotationSpeed;
		public float damage;

		void Update()
		{
			transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
		}

		void OnTriggerEnter2D(Collider2D other)
		{
			if (other.TryGetComponent(out Enemy enemy))
			{
				Debug.LogWarning("enemy hit circle: " + enemy.name);
			}
		}
	}
}
