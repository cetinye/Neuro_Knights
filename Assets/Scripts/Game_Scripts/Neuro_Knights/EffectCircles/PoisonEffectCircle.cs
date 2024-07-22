using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Neuro_Knights
{
	public class PoisonEffectCircle : MonoBehaviour
	{
		public float slowPower;
		public float slowDuration;
		public float radius;

		void Update()
		{
			OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), radius);
		}

		private void OverlapCircleAll(Vector2 position, float radius)
		{
			Collider2D[] colliders = Physics2D.OverlapCircleAll(position, radius);

			foreach (Collider2D collider in colliders)
			{
				if (collider.TryGetComponent(out Enemy enemy))
				{
					enemy.Poisoned(slowPower, slowDuration);
				}
			}
		}

		private void OnDrawGizmos()
		{
			Gizmos.DrawWireSphere(transform.position, radius);
		}
	}
}
