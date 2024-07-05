using UnityEngine;

namespace Neuro_Knights
{
	public class Trajectory : MonoBehaviour
	{
		[SerializeField] private Vector2 direction;
		[SerializeField] private float speed;
		[SerializeField] private float damage;

		private bool isMoveable = false;

		void Update()
		{
			if (isMoveable)
				transform.Translate(speed * Time.deltaTime * direction.normalized);
		}

		void OnTriggerEnter2D(Collider2D other)
		{
			if (other.TryGetComponent(out Player player))
			{
				// player.TakeDamage(damage);
				AudioManager.instance.PlayAt(SoundType.TrajectoryHit, 0.075f);
				Destroy(gameObject);
			}
		}

		public void SetDamage(float damage)
		{
			this.damage = damage;
		}

		public void SetDirection(Vector2 dir, Vector2 pos)
		{
			transform.position = pos;
			direction = dir;
			isMoveable = true;
		}
	}
}
