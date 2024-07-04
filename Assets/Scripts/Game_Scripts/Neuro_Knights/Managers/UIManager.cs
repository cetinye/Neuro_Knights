using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Neuro_Knights
{
	public class UIManager : MonoBehaviour
	{
		[SerializeField] private TMP_Text killedText;
		[SerializeField] private TMP_Text levelTimeText;
		[SerializeField] private GameObject upgradePanel;

		[Header("Flash Variables")]
		[SerializeField] private float flashInterval = 0.5f;
		private Color defaultColor;

		void Awake()
		{
			defaultColor = levelTimeText.color;
		}

		public void UpdateKilledText(int value)
		{
			killedText.text = "Killed: " + value.ToString();
		}

		public void UpdateLevelTime(float value)
		{
			levelTimeText.text = "Time Left: " + value.ToString("F0");
		}

		public void SetUpgradePanel(bool state)
		{
			upgradePanel.SetActive(state);
		}

		public void FlashRed()
		{
			Sequence redFlash = DOTween.Sequence();

			redFlash.Append(levelTimeText.DOColor(Color.red, flashInterval))
					.SetEase(Ease.Linear)
					.Append(levelTimeText.DOColor(defaultColor, flashInterval))
					.SetEase(Ease.Linear)
					.SetLoops(6);

			redFlash.Play();
		}
	}
}
