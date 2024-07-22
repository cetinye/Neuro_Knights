using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Neuro_Knights
{
	public class Pistol : Weapon
	{
		public bool isReadyToShoot = true;
		[SerializeField] private Transform nozzle;
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

		public override void Fire()
		{
			if (isReadyToShoot)
			{
				isReadyToShoot = false;

				LookAtEnemy();
				PlayFireSound();
				MuzzleFlash();
				SpawnBullet();
				Invoke(nameof(ReadyToShoot), fireRate);
			}
		}

		public void SpawnBullet()
		{
			Enemy closestEnemy = GetClosestEnemy();

			Bullet spawnedBullet = Instantiate(bullet, new Vector3(nozzle.position.x, nozzle.position.y, -0.5f), Quaternion.identity, nozzle.transform);

			spawnedBullet.SetDamage(damage);
			spawnedBullet.SetDirection(GetDirection());
		}

		public void ReadyToShoot()
		{
			isReadyToShoot = true;
		}

		private void MuzzleFlash()
		{
			ParticleSystem spawnedParticle = Instantiate(muzzleFlash, Vector3.zero, Quaternion.Euler(new Vector3(-90f, 0f, 0f)), nozzle.transform);
			spawnedParticle.transform.localPosition = Vector3.zero;
			spawnedParticle.transform.rotation = Quaternion.Euler(new Vector3(-90f, 0f, 0f));
			spawnedParticle.Play();
		}

		public Vector2 GetDirection()
		{
			Enemy enemy = GetClosestEnemy();

			Vector2 direction = new Vector2(enemy.transform.position.x - nozzle.position.x,
				enemy.transform.position.y - nozzle.position.y);

			return direction;
		}

		private void PlayFireSound()
		{
			AudioManager.instance.GetSoundSource(SoundType.PistolFire).pitch = 1 + Random.Range(-0.1f, 0.1f);
			AudioManager.instance.PlayOneShot(SoundType.PistolFire);
		}
	}
}
