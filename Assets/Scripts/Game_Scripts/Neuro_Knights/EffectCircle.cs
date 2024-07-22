using System.Collections.Generic;
using UnityEngine;

namespace Neuro_Knights
{
	public class EffectCircle : MonoBehaviour
	{
		public int damage;
		public int fireballCount;
		public float fireballRadius;
		public float rotationSpeed;
		public float burnDuration;
		public float burnInterval;

		[Space()]
		[SerializeField] private GameObject fireballPref;
		[SerializeField] private List<GameObject> fireballs = new List<GameObject>();

		void Start()
		{
			for (int i = 0; i < fireballCount; i++)
			{
				fireballs.Add(Instantiate(fireballPref, transform.position, Quaternion.identity, transform));
			}

			PositionFireballs();
		}

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

		void PositionFireballs()
		{
			float angleStep = 360f / fireballs.Count;
			Vector3 center = transform.position;

			for (int i = 0; i < fireballs.Count; i++)
			{
				// Calculate the angle for this fireball in radians
				float angle = i * angleStep * Mathf.Deg2Rad;

				// Calculate the position on the circle
				Vector3 newPosition = new Vector3(
					center.x + Mathf.Cos(angle) * fireballRadius,
					center.y + Mathf.Sin(angle) * fireballRadius,
					0
				);

				// Set the position of the fireball
				fireballs[i].transform.position = newPosition;

				// Calculate the direction from the center to the fireball
				Vector3 direction = (newPosition - center).normalized;

				// Rotate the fireball to face outward and adjust the local rotation
				fireballs[i].transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
				fireballs[i].transform.Rotate(0, 0, 230); // Adjust this value as needed to align the tail
			}
		}
	}
}
