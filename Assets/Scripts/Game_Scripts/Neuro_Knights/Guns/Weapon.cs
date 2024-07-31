using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Neuro_Knights
{
	public class Weapon : MonoBehaviour
	{
		public float damage;
		public float range;
		public float fireRate;
		public bool isReadyToShoot = true;
		public Transform nozzle;
		public ParticleSystem muzzleFlash;
		public Bullet bullet;
		public Trajectory trajectory;
		private LevelManager levelManager;

		void Awake()
		{
			isReadyToShoot = true;
		}

		void Start()
		{
			levelManager = LevelManager.instance;
		}

		void Update()
		{
			if (levelManager.GetSpawnedEnemies().Count == 0)
				return;

			if (GameStateManager.GetGameState() != GameState.Playing)
				return;

			Shoot();
		}

		public virtual void Fire()
		{

		}

		private void Shoot()
		{
			Enemy closestEnemy = GetClosestEnemy();

			if (closestEnemy != null)
			{
				if (closestEnemy.GetDistanceToPlayer() <= range)
				{
					Fire();
				}
			}
		}

		public void SpawnBullet()
		{
			Enemy closestEnemy = GetClosestEnemy();

			Bullet spawnedBullet = Instantiate(bullet, new Vector3(nozzle.position.x, nozzle.position.y, -0.5f), Quaternion.identity, nozzle.transform);

			spawnedBullet.SetDamage(damage);
			spawnedBullet.SetDirection(GetDirectionToEnemy());
		}

		public void SpawnTrajectory()
		{
			Trajectory spawnedTrajectory = Instantiate(trajectory, new Vector3(transform.position.x, transform.position.y, -1f), Quaternion.identity, null);
			spawnedTrajectory.SetDamage(damage);
			spawnedTrajectory.SetDirection(GetDirectionToPlayer(), transform.position);
		}

		public void ReadyToShoot()
		{
			isReadyToShoot = true;
		}

		public void MuzzleFlash()
		{
			ParticleSystem spawnedParticle = Instantiate(muzzleFlash, Vector3.zero, Quaternion.Euler(new Vector3(-90f, 0f, 0f)), nozzle.transform);
			spawnedParticle.transform.localPosition = Vector3.zero;
			spawnedParticle.transform.rotation = Quaternion.Euler(new Vector3(-90f, 0f, 0f));
			spawnedParticle.Play();
		}

		public void PlayFireSound()
		{
			AudioManager.instance.GetSoundSource(SoundType.PistolFire).pitch = 1 + Random.Range(-0.1f, 0.1f);
			AudioManager.instance.PlayOneShot(SoundType.PistolFire);
		}

		public void LookAtEnemy()
		{
			if (GetClosestEnemy() == null) return;

			Vector2 closestEnemyPos = GetClosestEnemy().transform.position;
			Vector2 targetPos;

			targetPos.x = closestEnemyPos.x - transform.position.x;
			targetPos.y = closestEnemyPos.y - transform.position.y;
			float angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
			// transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

			if (angle > 0)
			{
				if (angle < 270 && angle > 90)
				{
					transform.localScale = new Vector3(transform.localScale.x, -Mathf.Abs(transform.localScale.y), transform.localScale.z);
				}
				else
				{
					transform.localScale = new Vector3(transform.localScale.x, Mathf.Abs(transform.localScale.y), transform.localScale.z);
				}
			}
			else
			{
				if (angle > -270 && angle < -90)
				{
					transform.localScale = new Vector3(transform.localScale.x, -Mathf.Abs(transform.localScale.y), transform.localScale.z);
				}
				else
				{
					transform.localScale = new Vector3(transform.localScale.x, Mathf.Abs(transform.localScale.y), transform.localScale.z);
				}
			}

			// transform.DORotate(new Vector3(0, 0, angle), 0);
			transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
		}

		public Enemy GetClosestEnemy()
		{
			List<Enemy> enemies = LevelManager.instance.GetSpawnedEnemies();
			float minDistance = 10000;
			Enemy closestEnemy = null;

			foreach (Enemy enemy in enemies)
			{
				// enemy.GetComponent<SpriteRenderer>().color = Color.white;
				float distance = enemy.GetDistanceFrom(transform.position);
				if (distance < minDistance)
				{
					minDistance = distance;
					closestEnemy = enemy;
				}
			}

			// closestEnemy.GetComponent<SpriteRenderer>().color = Color.red;
			return closestEnemy;
		}

		public Vector2 GetDirectionToEnemy()
		{
			Enemy enemy = GetClosestEnemy();

			Vector2 direction = new Vector2(enemy.transform.position.x - nozzle.position.x,
				enemy.transform.position.y - nozzle.position.y);

			return direction;
		}

		public Vector2 GetDirectionToPlayer()
		{
			Vector2 direction = new Vector2(LevelManager.instance.GetPlayer().GetPlayerPosition().x - transform.position.x,
				LevelManager.instance.GetPlayer().GetPlayerPosition().y - transform.position.y);

			return direction;
		}
	}
}
