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

		[Header("Level Variables")]
		[SerializeField] private int levelId;
		[SerializeField] private LevelSO levelSO;
		[SerializeField] private List<LevelSO> levels = new List<LevelSO>();

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

		[Header("Flash Interval")]
		[SerializeField] private bool isFlashable = true;

		void Awake()
		{
			instance = this;

			GameStateManager.OnGameStateChanged += OnStateChange;
		}

		void OnDestroy()
		{
			GameStateManager.OnGameStateChanged -= OnStateChange;
		}

		void Start()
		{
			StartGame();
		}

		void Update()
		{
			LevelTimer();
		}

		void StartGame()
		{
			AssignLevelVariables();

			levelTimer = levelTime;
			GameStateManager.SetGameState(GameState.Playing);
			enemySpawner.SpawnEnemies(spawnInterval);
		}

		private void AssignLevelVariables()
		{
			levelSO = levels[levelId];

			levelTime = levelSO.levelTime;
		}

		private void OnStateChange()
		{
			switch (GameStateManager.GetGameState())
			{
				case GameState.Idle:
					break;

				case GameState.Playing:
					uiManager.SetUpgradePanel(false);
					isLevelTimerOn = true;
					break;

				case GameState.Upgrade:
					isLevelTimerOn = false;
					uiManager.SetUpgradePanel(true);
					break;
			}
		}

		private void LevelTimer()
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

			if (levelTimer <= 5.2f && isFlashable)
			{
				isFlashable = false;
				// GameManager.instance.PlayFx("Countdown", 0.7f, 1f);
				uiManager.FlashRed();
			}
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

			if (killedCount % 10 == 0)
			{
				GameStateManager.SetGameState(GameState.Upgrade);
			}
		}
	}
}
