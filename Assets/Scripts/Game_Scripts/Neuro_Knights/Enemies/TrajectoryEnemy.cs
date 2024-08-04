using UnityEngine;

namespace Neuro_Knights
{
	public class TrajectoryEnemy : Enemy
	{
		public TrajectoryMovementComponent movementComponent;
		public Weapon weapon;

		void Awake()
		{
			player = LevelManager.instance.GetPlayer();

			healthComponent.Initialize(maxHealth);
			movementComponent.Initialize(player, this);
			animationComponent.Initialize(spriteRenderer, xScaleAmount, xScaleTime, yScaleTime);
			effectComponent.Initialize(healthComponent, animationComponent, blood, burn, ice, iceScaleDuration, iceMeltDuration);
		}

		public override void FollowPlayer()
		{
			movementComponent.Movement();
		}
	}
}
