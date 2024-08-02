using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Neuro_Knights
{
	public class Player : MonoBehaviour
	{
		[SerializeField] private float health;
		[SerializeField] private SpriteRenderer body;
		[SerializeField] private SpriteRenderer cape;
		[SerializeField] private float xpPickupRange;

		[Header("Weapon Variables")]
		[SerializeField] private float gunRadius;
		[SerializeField] private Transform gunsTransform;
		[SerializeField] private List<Weapon> weapons;
		[SerializeField] private List<Weapon> spawnedGuns;

		[Header("Movement Variables")]
		[SerializeField] private float speed;
		[SerializeField] private VariableJoystick variableJoystick;
		[SerializeField] private float xBound;
		[SerializeField] private float yBound;

		[Header("Effect Variables")]
		[SerializeField] private Effect effect;

		[Header("Walk Anim Variables")]
		[SerializeField] private float xScaleAmount;
		[SerializeField] private float yScaleAmount;
		[SerializeField] private float xScaleTime;
		[SerializeField] private float yScaleTime;
		private Sequence walkSeq;

		[Header("Particles")]
		[SerializeField] private ParticleSystem healParticle;

		private LevelManager levelManager;
		private float xpAmount;
		private int xpLevel = 1;

		void Start()
		{
			levelManager = LevelManager.instance;

			WalkAnim();
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
				transform.position = new Vector3(Mathf.Clamp(transform.position.x, -xBound, xBound), Mathf.Clamp(transform.position.y, -yBound, yBound), -0.005f);
			}

			// if (direction.normalized != Vector3.zero)
			// 	transform.up = direction.normalized;

			if (direction.x > 0)
			{
				// body.transform.localRotation = Quaternion.Euler(new Vector3(body.transform.localRotation.eulerAngles.x, 180, body.transform.localRotation.eulerAngles.z));
				cape.transform.localRotation = Quaternion.Euler(new Vector3(cape.transform.localRotation.eulerAngles.x, 180, cape.transform.localRotation.eulerAngles.z));
			}

			else if (direction.x < 0)
			{
				// body.transform.localRotation = Quaternion.Euler(new Vector3(body.transform.localRotation.eulerAngles.x, 0, body.transform.localRotation.eulerAngles.z));
				cape.transform.localRotation = Quaternion.Euler(new Vector3(cape.transform.localRotation.eulerAngles.x, 0, cape.transform.localRotation.eulerAngles.z));
			}
		}

		public void SpawnGun()
		{
			spawnedGuns.Add(Instantiate(weapons[UnityEngine.Random.Range(0, weapons.Count)], gunsTransform));
			UpdateGunPositions();
		}

		void UpdateGunPositions()
		{
			float angleStep = 360f / spawnedGuns.Count;

			for (int i = 0; i < spawnedGuns.Count; i++)
			{
				float angle = i * angleStep * Mathf.Deg2Rad;
				Vector3 newPosition = new Vector3(
					gunsTransform.position.x + Mathf.Cos(angle) * gunRadius,
					gunsTransform.position.y + Mathf.Sin(angle) * gunRadius,
					0
				);

				spawnedGuns[i].transform.position = newPosition;
			}
		}

		public Vector2 GetPlayerPosition()
		{
			return transform.position;
		}

		public float GetPickupRange()
		{
			return xpPickupRange;
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

		public void Heal(float amount)
		{
			health += amount;
			healParticle.Play();
		}

		public void AddXP(float amount)
		{
			xpAmount += amount / xpLevel;

			if (xpAmount >= 100)
			{
				AudioManager.instance.PlayOneShot(SoundType.LevelUp);
				xpLevel++;
				levelManager.uiManager.FillAndSetExcessXP((xpAmount - 100) / 100);
				xpAmount -= 100;
			}
			else
			{
				levelManager.uiManager.SetXpSlider(xpAmount / 100);
			}
		}

		public int GetXPLevel()
		{
			return xpLevel;
		}

		public void SetSprite(Sprite newSprite)
		{
			body.sprite = newSprite;
		}

		public void SetCape(Sprite newCape)
		{
			cape.sprite = newCape;
		}

		public void SetEffect(Effect effect)
		{
			this.effect = Instantiate(effect, cape.transform.position, Quaternion.identity, transform);
		}

		private void WalkAnim()
		{
			walkSeq = DOTween.Sequence();

			walkSeq.Append(cape.transform.DOScaleX(cape.transform.localScale.x + xScaleAmount, xScaleTime));
			walkSeq.Join(cape.transform.DOScaleY(cape.transform.localScale.y - yScaleAmount, yScaleTime));

			walkSeq.SetLoops(-1, LoopType.Yoyo);
		}
	}
}
