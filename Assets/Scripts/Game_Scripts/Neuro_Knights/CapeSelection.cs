using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Neuro_Knights
{
	public class CapeSelection : MonoBehaviour
	{
		public void Clicked()
		{
			LevelManager.instance.ChooseCape(GetComponent<Image>().sprite);
			GameStateManager.SetGameState(GameState.Start);
		}
	}
}
