using UnityEngine;

namespace Neuro_Knights
{
	public class FollowerMovementComponent : MonoBehaviour, IMovement
	{
		public float speed;
		private Player player;
		float IMovement.speed { get => speed; set => speed = value; }

		public void Initialize(Player player)
		{
			this.player = player;
		}

		public void Movement()
		{
			transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, player.transform.position.y, -0.004f), speed * Time.deltaTime);
		}
	}
}
