using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Neuro_Knights
{
	public class Enemy : MonoBehaviour
	{
		[SerializeField] private float speed;
		[SerializeField] private float distanceToPlayer;

		private LevelManager levelManager;
		private Player player;
		private bool isFollowing = false;

		void Start()
		{
			levelManager = LevelManager.instance;
			player = levelManager.GetPlayer();
			isFollowing = true;
		}

		void Update()
		{
			if (isFollowing)
			{
				FollowPlayer();
				distanceToPlayer = GetDistanceToPlayer();
			}
		}

		private void FollowPlayer()
		{
			transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
			LookAtPlayer();
		}

		private void LookAtPlayer()
		{
			Vector2 targetPos;

			targetPos.x = player.transform.position.x - transform.position.x;
			targetPos.y = player.transform.position.y - transform.position.y;
			float angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
		}

		public float GetDistanceToPlayer()
		{
			return Vector2.Distance(transform.position, player.transform.position);
		}
	}
}
