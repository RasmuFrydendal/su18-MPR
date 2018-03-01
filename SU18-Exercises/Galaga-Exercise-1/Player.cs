using DIKUArcade.EventBus;

namespace Galaga_Exercise_1
{
    public class Player : IGameEventProcessor<object>
    {
        public Player()
        {
            
        }

        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent)
        {
            if (eventType == GameEventType.PlayerEvent)
            {
                    
            }
        }

    }
}