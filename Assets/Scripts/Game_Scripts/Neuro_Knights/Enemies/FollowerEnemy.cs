using DG.Tweening;
using UnityEngine;

namespace Neuro_Knights
{
	public class FollowerEnemy : Enemy
	{
		void Update()
		{
			if (isFollowing && health > 0 && GameStateManager.GetGameState() == GameState.Playing)
			{
				FollowPlayer();
				distanceToPlayer = GetDistanceToPlayer();
			}
		}

		public override void FollowPlayer()
		{
			// transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
			transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, player.transform.position.y, -0.004f), speed * Time.deltaTime);
			LookAtPlayer();
		}
	}
}
