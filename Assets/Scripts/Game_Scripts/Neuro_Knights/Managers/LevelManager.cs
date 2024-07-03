using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Neuro_Knights
{
	public class LevelManager : MonoBehaviour
	{
		public static LevelManager instance;
		public Camera mainCamera;

		[Header("Player Variables")]
		[SerializeField] private Player player;

		[Header("Enemy Spawner Variables")]
		[SerializeField] private EnemySpawner enemySpawner;
		[SerializeField] private float spawnInterval;

		void Awake()
		{
			instance = this;
		}

		void Start()
		{
			StartGame();
		}

		void StartGame()
		{
			enemySpawner.SpawnEnemies(spawnInterval);
		}

		public Player GetPlayer()
		{
			return player;
		}

		public List<Enemy> GetSpawnedEnemies()
		{
			return enemySpawner.GetSpawnedEnemies();
		}

		public EnemySpawner GetEnemySpawner()
		{
			return enemySpawner;
		}
	}
}
