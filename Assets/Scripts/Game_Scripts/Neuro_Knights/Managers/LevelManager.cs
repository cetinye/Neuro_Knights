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

		[Header("Level Time Variables")]
		[SerializeField] private float levelTime;
		private float levelTimer;
		private bool isLevelTimerOn;

		[Header("Wave Time Variables")]
		[SerializeField] private float waveTime;
		private float waveTimer;
		private bool isWaveTimerOn;

		[Header("Player Variables")]
		[SerializeField] private Player player;
		private int killedCount = 0;

		[Header("Enemy Spawner Variables")]
		[SerializeField] private EnemySpawner enemySpawner;

		[Header("DamagePopup Variables")]
		public DamagePopup damagePopupPrefab;

		[Header("Upgrade Variables")]
		[SerializeField] private int countForUpgrade;
		[SerializeField] private Upgrade upgradePrefab;
		[SerializeField] private List<UpgradeSO> lvl1upgrades = new List<UpgradeSO>();
		[SerializeField] private List<UpgradeSO> lvl2upgrades = new List<UpgradeSO>();
		[SerializeField] private List<UpgradeSO> lvl3upgrades = new List<UpgradeSO>();
		[SerializeField] private List<UpgradeSO> lvl4upgrades = new List<UpgradeSO>();

		[Header("Scene Object Variables")]
		[SerializeField] private int objectAmount;
		[SerializeField] private List<GameObject> objects = new List<GameObject>();
		[SerializeField] private Transform sceneTransform;

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
			WaveTimer();
		}

		void StartGame()
		{
			AssignLevelVariables();

			levelTimer = levelTime;
			waveTimer = waveTime;

			PopulateSceneWObjects();

			GameStateManager.SetGameState(GameState.CharacterSelection);
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
				case GameState.CharacterSelection:
					RemoveUpgradeCards();
					uiManager.SetCharacterSelectionPanel(true);
					break;

				case GameState.CapeSelection:
					RemoveUpgradeCards();
					uiManager.SetCharacterSelectionPanel(false);
					uiManager.SetCapeSelectionPanel(true);
					break;

				case GameState.Start:
					RemoveUpgradeCards();
					uiManager.SetCapeSelectionPanel(false);
					GameStateManager.SetGameState(GameState.Playing);
					enemySpawner.SpawnEnemies();
					break;

				case GameState.Playing:
					RemoveUpgradeCards();
					uiManager.SetCharacterSelectionPanel(false);
					uiManager.SetUpgradePanel(false);
					isLevelTimerOn = true;
					isWaveTimerOn = true;
					break;

				case GameState.Upgrade:
					isLevelTimerOn = false;
					isWaveTimerOn = false;
					SpawnUpgradeCards();
					uiManager.SetUpgradePanel(true);
					break;
			}
		}

		private void LevelTimer()
		{
			if (isLevelTimerOn)
			{
				levelTimer += Time.deltaTime;
				uiManager.UpdateLevelTime(levelTimer);
			}
		}

		private void WaveTimer()
		{
			if (isWaveTimerOn)
			{
				waveTimer -= Time.deltaTime;
				uiManager.UpdateWaveTime(waveTimer);
			}

			if (waveTimer < 0)
			{
				isWaveTimerOn = false;
				uiManager.UpdateWaveTime(0);

				enemySpawner.NextWave();
				waveTime += 10;
				waveTimer = waveTime;
				isWaveTimerOn = true;
			}

			if (waveTimer <= 5.2f && isFlashable)
			{
				isFlashable = false;
				// GameManager.instance.PlayFx("Countdown", 0.7f, 1f);
				uiManager.FlashRed();
			}
		}

		public void ChooseCharacter(Sprite chosenSp)
		{
			player.SetSprite(chosenSp);
		}

		public void ChooseCape(Sprite chosenSp)
		{
			player.SetCape(chosenSp);
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

			if (killedCount % countForUpgrade == 0)
			{
				GameStateManager.SetGameState(GameState.Upgrade);
			}
		}

		private void PopulateSceneWObjects()
		{
			for (int i = 0; i < objectAmount; i++)
			{
				GameObject chosenObj = objects[Random.Range(0, objects.Count)];
				Instantiate(chosenObj, enemySpawner.GetRandomSpawnPos(false), Quaternion.identity, sceneTransform);
			}
		}

		private void SpawnUpgradeCards()
		{
			List<UpgradeSO> selectedUpgrades = new List<UpgradeSO>();
			Upgrade spawnedUpgrade;
			UpgradeSO selectedCard;

			for (int i = 0; i < 4; i++)
			{
				spawnedUpgrade = Instantiate(upgradePrefab, uiManager.GetUpgradePanel(), false);

				do
				{
					selectedCard = lvl1upgrades[UnityEngine.Random.Range(0, lvl1upgrades.Count)];
				} while (selectedUpgrades.Contains(selectedCard));

				spawnedUpgrade.SetUpgrade(selectedCard);
				selectedUpgrades.Add(selectedCard);
			}
		}

		private void RemoveUpgradeCards()
		{
			for (int i = 0; i < uiManager.GetUpgradePanel().childCount; i++)
			{
				Destroy(uiManager.GetUpgradePanel().GetChild(i).gameObject);
			}
		}
	}
}
