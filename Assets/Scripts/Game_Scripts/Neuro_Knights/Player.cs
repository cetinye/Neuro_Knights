using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Neuro_Knights
{
	public class Player : MonoBehaviour
	{
		public float speed;
		public VariableJoystick variableJoystick;
		public Rigidbody2D rb;

		[SerializeField] private float xBound;
		[SerializeField] private float yBound;
		private LevelManager levelManager;

		void Start()
		{
			levelManager = LevelManager.instance;
		}

		public void Update()
		{
			PlayerMovement();
			LookAtEnemy();
		}

		private void PlayerMovement()
		{
			Vector3 direction = Vector3.up * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
			transform.position += speed * Time.deltaTime * direction.normalized;

			if (Mathf.Abs(transform.position.x) > xBound || Mathf.Abs(transform.position.y) > yBound)
			{
				transform.position = new Vector3(Mathf.Clamp(transform.position.x, -xBound, xBound), Mathf.Clamp(transform.position.y, -yBound, yBound), 0);
			}
		}

		private void LookAtEnemy()
		{
			if (levelManager.GetSpawnedEnemies().Count == 0)
				return;

			Vector2 closestEnemyPos = GetClosestEnemy().transform.position;
			Vector2 targetPos;

			targetPos.x = closestEnemyPos.x - transform.position.x;
			targetPos.y = closestEnemyPos.y - transform.position.y;
			float angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
		}

		public Vector2 GetPlayerPosition()
		{
			return transform.position;
		}

		private Enemy GetClosestEnemy()
		{
			List<Enemy> enemies = levelManager.GetSpawnedEnemies();
			float minDistance = 10000;
			Enemy closestEnemy = null;

			foreach (Enemy enemy in enemies)
			{
				enemy.GetComponent<SpriteRenderer>().color = Color.white;
				float distance = enemy.GetDistanceToPlayer();
				if (distance < minDistance)
				{
					minDistance = distance;
					closestEnemy = enemy;
				}
			}

			closestEnemy.GetComponent<SpriteRenderer>().color = Color.red;
			return closestEnemy;
		}
	}
}
