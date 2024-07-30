using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Neuro_Knights
{
	public class Upgrade : MonoBehaviour
	{
		[SerializeField] private UpgradeSO upgradeSO;
		[SerializeField] private Image cardImage;
		[SerializeField] private Image iconImage;
		[SerializeField] private TMP_Text descText;

		public void Initialize()
		{
			cardImage.sprite = upgradeSO.cardImage;
			iconImage.sprite = upgradeSO.iconImage;
			descText.text = "+" + upgradeSO.GetValue().ToString() + " " + upgradeSO.GetStatType().ToString();
		}

		public void SetUpgrade(UpgradeSO newUpgradeSO)
		{
			upgradeSO = newUpgradeSO;
			Initialize();
		}

		public void Clicked()
		{
			GameStateManager.SetGameState(GameState.Playing);
		}
	}
}
