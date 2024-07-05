using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Neuro_Knights
{
	public class TrajectoryGun : Weapon
	{
		public bool isReadyToShoot = true;

		void Awake()
		{
			isReadyToShoot = true;
		}

		public override void Fire()
		{
			if (isReadyToShoot)
			{
				// PlayFireSound();
				// MuzzleFlash();
				isReadyToShoot = false;
				SpawnTrajectory();
				Invoke(nameof(ReadyToShoot), fireRate);
			}
		}

		public void SpawnTrajectory()
		{
			Trajectory spawnedTrajectory = Instantiate(trajectory, new Vector3(transform.position.x, transform.position.y, -1f), Quaternion.identity, null);
			spawnedTrajectory.SetDamage(damage);
			spawnedTrajectory.SetDirection(GetDirection(), transform.position);
		}

		public Vector2 GetDirection()
		{
			Vector2 direction = new Vector2(LevelManager.instance.GetPlayer().GetPlayerPosition().x - transform.position.x,
				LevelManager.instance.GetPlayer().GetPlayerPosition().y - transform.position.y);

			return direction;
		}

		public void ReadyToShoot()
		{
			isReadyToShoot = true;
		}

		private void MuzzleFlash()
		{
			ParticleSystem spawnedParticle = Instantiate(muzzleFlash, Vector3.zero, Quaternion.Euler(new Vector3(-90f, 0f, 0f)), transform);
			spawnedParticle.transform.localPosition = Vector3.zero;
			spawnedParticle.transform.rotation = Quaternion.Euler(new Vector3(-90f, 0f, 0f));
			spawnedParticle.Play();
		}

		private void PlayFireSound()
		{
			AudioManager.instance.GetSoundSource(SoundType.PistolFire).pitch = 1 + Random.Range(-0.1f, 0.1f);
			AudioManager.instance.PlayOneShot(SoundType.PistolFire);
		}
	}
}
