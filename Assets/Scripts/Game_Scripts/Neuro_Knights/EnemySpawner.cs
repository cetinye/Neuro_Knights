using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Neuro_Knights
{
	public class EnemySpawner : MonoBehaviour
	{
		[SerializeField] private List<Enemy> enemyPrefabs = new List<Enemy>();
		[SerializeField] private float range;
		[SerializeField] private List<Enemy> spawnedEnemies = new List<Enemy>();

		private LevelManager levelManager;

		void Start()
		{
			levelManager = LevelManager.instance;
		}

		public void SpawnEnemies(float interval)
		{
			InvokeRepeating(nameof(SpawnEnemy), interval, interval);
		}

		private void SpawnEnemy()
		{
			Enemy enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], GetSpawnPosition(), Quaternion.identity);
			enemy.transform.position = new Vector3(enemy.transform.position.x, enemy.transform.position.y, -0.004f);
			spawnedEnemies.Add(enemy);
		}

		private Vector2 GetSpawnPosition()
		{
			Vector2 vector2 = new Vector2(GetXPos(), GetYPos());
			return vector2;
		}

		private float GetXPos()
		{
			Vector2 playerPos = levelManager.GetPlayer().GetPlayerPosition();
			float randomMinX = UnityEngine.Random.Range(playerPos.x - (range * 1.5f), playerPos.x - range);
			float randomMaxX = UnityEngine.Random.Range(playerPos.x + range, playerPos.x + (range * 1.5f));
			return UnityEngine.Random.Range(randomMinX, randomMaxX);
		}

		private float GetYPos()
		{
			Vector2 playerPos = levelManager.GetPlayer().GetPlayerPosition();
			float randomMinY = UnityEngine.Random.Range(playerPos.y - (range * 1.5f), playerPos.y - range);
			float randomMaxY = UnityEngine.Random.Range(playerPos.y + range, playerPos.y + (range * 1.5f));
			return UnityEngine.Random.Range(randomMinY, randomMaxY);
		}

		public List<Enemy> GetSpawnedEnemies()
		{
			return spawnedEnemies;
		}

		public void RemoveEnemy(Enemy enemy)
		{
			spawnedEnemies.Remove(enemy);
		}
	}
}
