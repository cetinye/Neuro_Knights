using System;

namespace Neuro_Knights
{
    public static class GameStateManager
    {
        private static GameState gameState = GameState.None;
        public static event Action OnGameStateChanged;

        public static void SetGameState(GameState newGameState)
        {
            gameState = newGameState;
            OnGameStateChanged?.Invoke();
        }

        public static GameState GetGameState()
        {
            return gameState;
        }
    }

    public enum GameState
    {
        None,
        CharacterSelection,
        Start,
        Playing,
        Upgrade,
    }
}