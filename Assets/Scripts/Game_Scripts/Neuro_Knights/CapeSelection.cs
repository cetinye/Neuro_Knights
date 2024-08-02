using UnityEngine;
using UnityEngine.UI;

namespace Neuro_Knights
{
	public class CapeSelection : MonoBehaviour
	{
		[SerializeField] private Effect effect;

		public void Clicked()
		{
			LevelManager.instance.ChooseCape(GetComponent<Image>().sprite);
			LevelManager.instance.GetPlayer().SetEffect(effect);
			GameStateManager.SetGameState(GameState.Start);
		}
	}
}
