using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Neuro_Knights
{
	public class Bullet : MonoBehaviour
	{
		[SerializeField] private Enemy target;
		[SerializeField] private float speed;
		[SerializeField] private float damage;

		void Update()
		{
			if (target != null)
			{
				transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

				if (Vector2.Distance(transform.position, target.transform.position) <= 0.1f)
				{
					target.TakeDamage(damage);
					Destroy(gameObject);
				}
			}
			else
			{
				Destroy(gameObject);
			}
		}

		public void SetDamage(float damage)
		{
			this.damage = damage;
		}

		public void SetTarget(Enemy enemy)
		{
			target = enemy;
		}
	}
}
