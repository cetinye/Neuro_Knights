namespace Neuro_Knights
{
	public class Gun : Weapon
	{
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
	}
}
