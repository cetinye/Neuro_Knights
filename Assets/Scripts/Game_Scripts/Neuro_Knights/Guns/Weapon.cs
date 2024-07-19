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
		public ParticleSystem muzzleFlash;
		public Bullet bullet;
		public Trajectory trajectory;

		public virtual void Fire()
		{

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

			transform.DORotate(new Vector3(0, 0, angle), 0);
		}

		public Enemy GetClosestEnemy()
		{
			List<Enemy> enemies = LevelManager.instance.GetSpawnedEnemies();
			float minDistance = 10000;
			Enemy closestEnemy = null;

			foreach (Enemy enemy in enemies)
			{
				// enemy.GetComponent<SpriteRenderer>().color = Color.white;
				float distance = enemy.GetDistanceToPlayer();
				if (distance < minDistance && enemy.GetInstantHealth() > 0)
				{
					minDistance = distance;
					closestEnemy = enemy;
				}
			}

			// closestEnemy.GetComponent<SpriteRenderer>().color = Color.red;
			return closestEnemy;
		}

		public void CheckTarget(Enemy enemy, float damage)
		{
			if (enemy.GetInstantHealth() > 0)
				enemy.InstantHealthDamage(damage);
		}
	}
}
