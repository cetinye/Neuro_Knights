using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Neuro_Knights
{
	public class FollowPlayer : MonoBehaviour
	{
		[SerializeField] private Player player;

		void LateUpdate()
		{
			transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10f);
		}
	}
}
