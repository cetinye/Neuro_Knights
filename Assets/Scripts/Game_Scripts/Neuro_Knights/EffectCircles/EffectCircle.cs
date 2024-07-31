using System.Collections.Generic;
using UnityEngine;

namespace Neuro_Knights
{
	public class EffectCircle : MonoBehaviour
	{
		public EffectType effectType;
		public int damage;
		public int ballCount;
		public float ballRadius;
		public float rotationSpeed;
		public float effectDuration;
		public float effectInterval;

		[Space()]
		[SerializeField] private GameObject ballPref;
		[SerializeField] private List<GameObject> balls = new List<GameObject>();

		void Start()
		{
			for (int i = 0; i < ballCount; i++)
			{
				balls.Add(Instantiate(ballPref, transform.position, Quaternion.identity, transform));
			}

			PositionBalls();
		}

		void Update()
		{
			transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
		}

		void OnTriggerEnter2D(Collider2D other)
		{
			if (other.TryGetComponent(out Enemy enemy))
			{
				switch (effectType)
				{
					case EffectType.Fire:
						enemy.PlayBurnParticle(damage, effectDuration, effectInterval);
						break;

					case EffectType.Ice:
						enemy.PlayIceParticle(damage, effectDuration);
						break;
				}
			}
		}

		void PositionBalls()
		{
			float angleStep = 360f / balls.Count;
			Vector3 center = transform.position;

			for (int i = 0; i < balls.Count; i++)
			{
				// Calculate the angle for this fireball in radians
				float angle = i * angleStep * Mathf.Deg2Rad;

				// Calculate the position on the circle
				Vector3 newPosition = new Vector3(
					center.x + Mathf.Cos(angle) * ballRadius,
					center.y + Mathf.Sin(angle) * ballRadius,
					0
				);

				// Set the position of the fireball
				balls[i].transform.position = newPosition;

				// Calculate the direction from the center to the fireball
				Vector3 direction = (newPosition - center).normalized;

				// Rotate the fireball to face outward and adjust the local rotation
				balls[i].transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
				balls[i].transform.Rotate(0, 0, 230); // Adjust this value as needed to align the tail
			}
		}
	}

	public enum EffectType
	{
		Fire,
		Ice
	}
}
