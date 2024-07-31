namespace Neuro_Knights
{
	public class TrajectoryGun : Weapon
	{
		public override void Fire()
		{
			if (isReadyToShoot)
			{
				isReadyToShoot = false;

				Enemy parent = transform.parent.TryGetComponent<Enemy>(out parent) ? parent : null;
				Invoke(nameof(ReadyToShoot), fireRate);

				if (parent.isFrozen) return;

				SpawnTrajectory();
			}
		}
	}
}
