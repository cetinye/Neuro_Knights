using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Neuro_Knights
{
	public class FollowPlayer : MonoBehaviour
	{
		[SerializeField] private Player player;
		[SerializeField] private float xBound;
		[SerializeField] private float yBound;

		void LateUpdate()
		{
			transform.position = new Vector3(Mathf.Clamp(player.transform.position.x, -xBound, xBound), Mathf.Clamp(player.transform.position.y, -yBound, yBound), -10f);
		}
	}
}
