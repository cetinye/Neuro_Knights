using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Neuro_Knights
{
	public class TrajectoryMovementComponent : MonoBehaviour, IMovement
	{
		public float speed;
		private float distanceToStop;
		private Player player;
		float IMovement.speed { get => speed; set => speed = value; }

		public void Initialize(Player player, TrajectoryEnemy enemy)
		{
			this.player = player;
			distanceToStop = enemy.weapon.range;
		}

		public void Movement()
		{
			TryGetComponent(out Enemy enemy);

			if (enemy.GetDistanceToPlayer() < distanceToStop) return;

			transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, player.transform.position.y, -0.004f), speed * Time.deltaTime);
		}

		public void LookAtPlayer()
		{
			Vector2 targetPos;
			targetPos.x = player.transform.position.x - transform.position.x;

			if (targetPos.x > 0)
			{
				transform.localRotation = Quaternion.Euler(new Vector3(transform.localRotation.eulerAngles.x, 0, transform.localRotation.eulerAngles.z));
			}

			else if (targetPos.x < 0)
			{
				transform.localRotation = Quaternion.Euler(new Vector3(transform.localRotation.eulerAngles.x, 180, transform.localRotation.eulerAngles.z));
			}
		}
	}
}
