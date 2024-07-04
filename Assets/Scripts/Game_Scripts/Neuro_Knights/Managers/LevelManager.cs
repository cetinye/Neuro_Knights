using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Neuro_Knights
{
	public class LevelManager : MonoBehaviour
	{
		public static LevelManager instance;
		public UIManager uiManager;
		public Camera mainCamera;

		[Header("Time Variables")]
		[SerializeField] private float levelTime;
		private float levelTimer;
		private bool isLevelTimerOn;

		[Header("Player Variables")]
		[SerializeField] private Player player;
		private int killedCount = 0;

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

		void Update()
		{
			if (isLevelTimerOn)
			{
				levelTimer -= Time.deltaTime;
				uiManager.UpdateLevelTime(levelTimer);
			}

			if (levelTimer < 0)
			{
				isLevelTimerOn = false;
				uiManager.UpdateLevelTime(0);
			}
		}

		void StartGame()
		{
			levelTimer = levelTime;
			isLevelTimerOn = true;
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

		public void EnemyKilled()
		{
			killedCount++;
			uiManager.UpdateKilledText(killedCount);
		}
	}
}
