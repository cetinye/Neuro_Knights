using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Neuro_Knights
{
	public class XP : MonoBehaviour
	{
		[SerializeField] private int xpAmount;
		[SerializeField] private float speed;
		private bool isCollected = false;
		private Player player;

		void Update()
		{
			if (isCollected && GameStateManager.GetGameState() == GameState.Playing)
			{
				transform.position = Vector2.MoveTowards(transform.position, LevelManager.instance.GetPlayer().GetPlayerPosition(), speed * Time.deltaTime);

				if (Vector2.Distance(transform.position, LevelManager.instance.GetPlayer().GetPlayerPosition()) <= 0.1f)
				{
					player.AddXP(xpAmount);
					Destroy(gameObject);
				}
			}
		}

		void OnTriggerEnter2D(Collider2D other)
		{
			if (other.TryGetComponent(out Player player) && !isCollected)
			{
				isCollected = true;
				this.player = player;
			}
		}
	}
}
