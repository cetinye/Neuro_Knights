using UnityEngine;

namespace Neuro_Knights
{
	public class Bullet : MonoBehaviour
	{
		[SerializeField] private float speed;
		[SerializeField] private float damage;
		[SerializeField] private Vector3 direction;
		private bool isMoveable = false;

		void Update()
		{
			if (isMoveable && GameStateManager.GetGameState() == GameState.Playing)
				transform.position += speed * Time.deltaTime * direction.normalized;
		}

		void OnTriggerEnter2D(Collider2D other)
		{
			if (other.gameObject.TryGetComponent(out Enemy enemy))
			{
				enemy.TakeDamage(damage);
				Destroy(gameObject);
			}
		}

		public void SetDamage(float damage)
		{
			this.damage = damage;
		}

		public void SetDirection(Vector2 dir)
		{
			direction = dir;
			isMoveable = true;
		}
	}
}
