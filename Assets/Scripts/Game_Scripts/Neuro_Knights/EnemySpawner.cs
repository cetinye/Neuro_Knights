using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Neuro_Knights
{
	public class EnemySpawner : MonoBehaviour
	{
		[SerializeField] private List<Enemy> enemyPrefabs = new List<Enemy>();
		[SerializeField] private float range;
		[SerializeField] private List<Enemy> spawnedEnemies = new List<Enemy>();
		[SerializeField] private CrossMark crossMark;

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
			if (GameStateManager.GetGameState() != GameState.Playing) return;

			StartCoroutine(SpawnEnemyRoutine());
		}

		IEnumerator SpawnEnemyRoutine()
		{
			Vector2 spawnPosition = GetSpawnPosition();
			CrossMark spawnedCross = SpawnCrossMark(spawnPosition);
			yield return spawnedCross.AnimSequence().WaitForCompletion();

			if (spawnedCross.IsInterrupted()) yield break;

			Enemy enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], spawnPosition, Quaternion.identity);
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
			if (!spawnedEnemies.Contains(enemy)) return;

			spawnedEnemies.Remove(enemy);
			levelManager.EnemyKilled();
		}

		public CrossMark SpawnCrossMark(Vector3 position)
		{
			return Instantiate(crossMark, position, Quaternion.identity);
		}
	}
}
