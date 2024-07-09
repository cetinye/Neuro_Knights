using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Neuro_Knights
{
	public class UIManager : MonoBehaviour
	{
		[SerializeField] private TMP_Text killedText;
		[SerializeField] private TMP_Text levelTimeText;
		[SerializeField] private GameObject upgradePanel;
		[SerializeField] private Slider xpSlider;
		[SerializeField] private TMP_Text xpSliderLvlText;

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

		public void SetXpSlider(float value)
		{
			xpSlider.DOValue(value, 0.2f);
		}

		public void FillAndSetExcessXP(float excessXp)
		{
			xpSlider.DOValue(xpSlider.maxValue, 0.2f).OnComplete(() =>
			{
				xpSliderLvlText.text = "Level " + LevelManager.instance.GetPlayer().GetXPLevel();
				xpSlider.DOValue(excessXp, 0.2f);
			});
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
