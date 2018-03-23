using System;
using System.Threading;
using DIKUArcade.EventBus;
using DIKUArcade.State;

namespace Galaga_Exercise_3.GalagaState {
    
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
                ActiveState = GameRunning.GetInstance();
                break;
            case GameStateType.MainMenu:
                ActiveState = MainMenu.GetInstance();
                break;
            default:
                throw new AbandonedMutexException();
                    
            }
        }

        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent) {
            switch (eventType) {
            case GameEventType.GameStateEvent:
                SwitchState(StateTransformer.TransformStringToState(gameEvent.Parameter1));
                break;
            default:
                break;
            }
        }
    }
}

namespace Galaga_Exercise_3 {
    internal class GameStateType { }
}