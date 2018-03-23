using DIKUArcade.State;

namespace Galaga_Exercise_3 {
    public class GameRunning : IGameState {

        private static GameRunning instance = null;

        public GameRunning() {
            GameRunning.instance = this;
        }
        
        public void GameLoop() {
            ;
        }

        public void InitializeGameState() {
            GameRunning.instance = this;
        }

        public void UpdateGameLogic() {
        }

        public void RenderState() {
        }

        public void HandleKeyEvent(string keyValue, string keyAction) {
            
        }

        public static IGameState GetInstance() {
            return GameRunning.instance ?? new GameRunning();
        }
    }
}