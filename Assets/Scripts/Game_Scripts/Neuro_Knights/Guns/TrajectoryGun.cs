namespace Neuro_Knights
{
	public class TrajectoryGun : Weapon
	{
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
	}
}
