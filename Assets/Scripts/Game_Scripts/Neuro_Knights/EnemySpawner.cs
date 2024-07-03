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
			spawnedEnemies.Add(enemy);
		}

		private Vector2 GetSpawnPosition()
		{
			Vector2 playerPos = levelManager.GetPlayer().GetPlayerPosition();
			Vector2 vector2 = new Vector2(UnityEngine.Random.Range(playerPos.x - range, playerPos.x + range), UnityEngine.Random.Range(playerPos.y - range, playerPos.y + range));
			return vector2;
		}

		public List<Enemy> GetSpawnedEnemies()
		{
			return spawnedEnemies;
		}
	}
}
