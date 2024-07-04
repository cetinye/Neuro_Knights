using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Neuro_Knights
{
	public class Upgrade : MonoBehaviour
	{
		[SerializeField] private Image image;
		[SerializeField] private TMP_Text text;

		public void Clicked()
		{
			GameStateManager.SetGameState(GameState.Playing);
		}
	}
}
