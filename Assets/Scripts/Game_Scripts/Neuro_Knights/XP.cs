using UnityEngine;

namespace Neuro_Knights
{
	public class XP : MonoBehaviour
	{
		public float distToPlayer;
		[SerializeField] private int xpAmount;
		[SerializeField] private float speed;
		private bool isCollected = false;
		private Player player;

		void Start()
		{
			player = LevelManager.instance.GetPlayer();
		}

		void Update()
		{
			if (GameStateManager.GetGameState() != GameState.Playing)
				return;

			Vector3 playerPosition = player.GetPlayerPosition();
			distToPlayer = Vector2.Distance(transform.position, playerPosition);

			if (isCollected)
			{
				MoveTowardsPlayer(playerPosition);
			}
			else
			{
				CheckIfCollectable(playerPosition);
			}
		}

		void MoveTowardsPlayer(Vector3 playerPosition)
		{
			transform.position = Vector3.MoveTowards(transform.position, playerPosition, speed * Time.deltaTime);

			if (distToPlayer <= 0.1f)
			{
				AudioManager.instance.PlayOneShot(SoundType.XPPickup);
				player.AddXP(xpAmount);
				Destroy(gameObject);
			}
		}

		void CheckIfCollectable(Vector3 playerPosition)
		{
			if (distToPlayer <= player.GetPickupRange())
			{
				isCollected = true;
			}
		}
	}
}