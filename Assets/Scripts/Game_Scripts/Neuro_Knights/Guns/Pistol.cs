using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Neuro_Knights
{
	public class Pistol : Weapon
	{
		public bool isReadyToShoot = true;

		public override void Fire()
		{
			if (isReadyToShoot)
			{
				PlayFireSound();
				MuzzleFlash();
				isReadyToShoot = false;
				SpawnBullet();
				Invoke(nameof(ReadyToShoot), fireRate);
			}
		}

		public void SpawnBullet()
		{
			Bullet spawnedBullet = Instantiate(bullet, new Vector3(transform.position.x, transform.position.y, -1f), Quaternion.identity, transform);
			spawnedBullet.SetDamage(damage);
			spawnedBullet.SetTarget(LevelManager.instance.GetPlayer().GetClosestEnemy());
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
