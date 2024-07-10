using UnityEngine;
using UnityEngine.UI;

namespace Neuro_Knights
{
	public class CharacterSelection : MonoBehaviour
	{
		public void Clicked()
		{
			LevelManager.instance.ChooseCharacter(GetComponent<Image>().sprite);
			GameStateManager.SetGameState(GameState.Start);
		}
	}
}
