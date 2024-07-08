using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Neuro_Knights
{
	public class Player : MonoBehaviour
	{
		[SerializeField] private float radius;
		[SerializeField] private int gunAmount;
		[SerializeField] private List<Weapon> weapons;
		[SerializeField] private List<Weapon> spawnedGuns;
		[SerializeField] private List<Transform> weaponSlots;
		[SerializeField] private float speed;
		[SerializeField] private VariableJoystick variableJoystick;
		[SerializeField] private Rigidbody2D rb;
		[SerializeField] private float xBound;
		[SerializeField] private float yBound;
		private LevelManager levelManager;

		void Start()
		{
			levelManager = LevelManager.instance;
		}

		public void Update()
		{
			if (GameStateManager.GetGameState() != GameState.Playing) return;

			PlayerMovement();
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

		public void SpawnGun()
		{
			spawnedGuns.Add(Instantiate(weapons[0], transform));
			UpdateGunPositions();
		}

		void UpdateGunPositions()
		{
			float angleStep = 360f / spawnedGuns.Count;

			for (int i = 0; i < spawnedGuns.Count; i++)
			{
				float angle = i * angleStep * Mathf.Deg2Rad;
				Vector3 newPosition = new Vector3(
					transform.position.x + Mathf.Cos(angle) * radius,
					transform.position.y + Mathf.Sin(angle) * radius,
					0
				);

				spawnedGuns[i].transform.position = newPosition;
			}
		}

		public Vector2 GetPlayerPosition()
		{
			return transform.position;
		}

		public Enemy GetClosestEnemy()
		{
			List<Enemy> enemies = levelManager.GetSpawnedEnemies();
			float minDistance = 10000;
			Enemy closestEnemy = null;

			foreach (Enemy enemy in enemies)
			{
				// enemy.GetComponent<SpriteRenderer>().color = Color.white;
				float distance = enemy.GetDistanceToPlayer();
				if (distance < minDistance)
				{
					minDistance = distance;
					closestEnemy = enemy;
				}
			}

			// closestEnemy.GetComponent<SpriteRenderer>().color = Color.red;
			return closestEnemy;
		}
	}
}
