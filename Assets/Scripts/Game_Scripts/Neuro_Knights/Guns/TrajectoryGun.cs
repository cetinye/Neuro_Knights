namespace Neuro_Knights
{
	public class TrajectoryGun : Weapon
	{
		public override void Fire()
		{
			if (isReadyToShoot)
			{
				isReadyToShoot = false;

				EffectComponent parent = transform.parent.TryGetComponent<EffectComponent>(out parent) ? parent : null;
				Invoke(nameof(ReadyToShoot), fireRate);

				if (parent.isFrozen) return;

				SpawnTrajectory();
			}
		}
	}
}
