using System;

namespace Galaga_Exercise_3.GalagaState {
    
    public enum GameStateType {
        MainMenu,
        GamePaused,
        GameRunning
    }


    public static class StateTransformer {
        public static GameStateType TransformStringToState(string gameState) {
            switch (gameState) {
            case "MAIN_MENU":
                return GameStateType.MainMenu;
            case "GAME_RUNNING":
                return GameStateType.GameRunning;
            case "GAME_PAUSED":
                return GameStateType.GamePaused;
            default:
                throw new ArgumentException();
            }
        }
            
        public static string TransformStateToString(GameStateType gameState) {
            switch (gameState) {
            case GameStateType.MainMenu:
                return "MAIN_MENU";
            case GameStateType.GameRunning:
                return "GAME_RUNNING";
            case GameStateType.GamePaused:
                return "GAME_PAUSED";
            default:
                throw new ArgumentException();
            }
        }
    }
        
}