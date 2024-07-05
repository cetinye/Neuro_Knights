using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Neuro_Knights
{
	public class TrajectoryEnemy : Enemy
	{
		[SerializeField] private Weapon weapon;
		[SerializeField] private float range;
		[SerializeField] private float distanceToStop;

		void Update()
		{
			if (isFollowing && health > 0 && GameStateManager.GetGameState() == GameState.Playing)
			{
				distanceToPlayer = GetDistanceToPlayer();
				LookAtPlayer();
				Shoot();

				if (distanceToPlayer < distanceToStop) return;

				FollowPlayer();
			}
		}

		public override void FollowPlayer()
		{
			// transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
			transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, player.transform.position.y, -0.004f), speed * Time.deltaTime);
		}

		private void Shoot()
		{
			if (Vector2.Distance(transform.position, player.GetPlayerPosition()) <= range)
			{
				Debug.Log("Shoot");
				weapon.Fire();
			}
		}
	}
}
