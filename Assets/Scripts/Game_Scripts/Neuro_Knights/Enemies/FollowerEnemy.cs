namespace Neuro_Knights
{
	public class FollowerEnemy : Enemy
	{
		public FollowerMovementComponent movementComponent;

		void Awake()
		{
			player = LevelManager.instance.GetPlayer();

			healthComponent.Initialize(maxHealth);
			movementComponent.Initialize(player);
			animationComponent.Initialize(spriteRenderer, xScaleAmount, xScaleTime, yScaleTime);
			effectComponent.Initialize(healthComponent, animationComponent, blood, burn, ice, iceScaleDuration, iceMeltDuration);
		}

		public override void FollowPlayer()
		{
			movementComponent.Movement();
		}
	}
}
