using System.Runtime.Remoting.Messaging;
using DIKUArcade.EventBus;
using DIKUArcade.State;
using Galaga_Exercise_3.GalagaStates;

namespace Galaga_Exercise_3 {
        public class StateMachine : IGameEventProcessor<object> {
            public IGameState ActiveState { get; private set; }
            public StateMachine() {
                GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
                GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, this);
                ActiveState = MainMenu.GetInstance();
            }
            
            private void SwitchState(GameStateType stateType) {
                switch (stateType) {
                case GameStateType.GameRunning:
                    ActiveState = GameRunning.GetInstance() ?? (new MainMenu());
                    break;
                case GameStateType.MainMenu:
                    ActiveState = MainMenu.GetInstance() ?? (new MainMenu());
                    break;
                default:
                    ActiveState = MainMenu.GetInstance() ?? (new MainMenu());
                    break;
                    
                }
            }

            public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent) {
                
            }
        }
}