using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Neuro_Knights
{
	public class HealthBar : MonoBehaviour
	{
		[SerializeField] private Slider healthSlider;

		private Canvas canvas;

		void Awake()
		{
			canvas = GetComponent<Canvas>();
			canvas.worldCamera = Camera.main;
		}

		public void SetHealth(float health)
		{
			healthSlider.DOValue(health, 0.2f);
		}
	}
}
