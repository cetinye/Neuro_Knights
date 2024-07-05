using System.Collections;
using System.Collections.Generic;
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
	}
}
