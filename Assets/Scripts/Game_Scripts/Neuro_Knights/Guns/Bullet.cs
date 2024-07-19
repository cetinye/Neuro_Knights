using UnityEngine;

namespace Neuro_Knights
{
	public class Bullet : MonoBehaviour
	{
		[SerializeField] private Enemy target;
		[SerializeField] private float speed;
		[SerializeField] private float damage;
		private bool isReadyToShoot = false;

		void Update()
		{
			if (isReadyToShoot && target != null)
			{
				transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

				if (Vector2.Distance(transform.position, target.transform.position) <= 0.1f)
				{
					target.TakeDamage(damage);
					DamagePopup.Create(transform.position, damage);
					Destroy(gameObject);
				}
			}
			else if (isReadyToShoot && target == null)
				Destroy(gameObject);
		}

		public void SetDamage(float damage)
		{
			this.damage = damage;
		}

		public void SetTarget(Enemy enemy)
		{
			target = enemy;
			isReadyToShoot = true;
		}
	}
}
