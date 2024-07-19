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
		private Vector3 offset;

		void Awake()
		{
			offset = transform.position;
		}

		void LateUpdate()
		{
			transform.position = new Vector3(Mathf.Clamp(player.transform.position.x + offset.x, -xBound, xBound), Mathf.Clamp(player.transform.position.y + offset.y, -yBound, yBound), -10f);
		}
	}
}
