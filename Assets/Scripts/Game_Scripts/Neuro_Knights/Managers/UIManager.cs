using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Neuro_Knights
{
	public class UIManager : MonoBehaviour
	{
		[SerializeField] private TMP_Text killedText;
		[SerializeField] private TMP_Text levelTimeText;
		[SerializeField] private GameObject upgradePanel;

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
	}
}
