using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;


namespace Galaga_Exercise_1
{
    public class Player : IGameEventProcessor<object>
    {

        private Image projectile;
        
        private Entity player;

        public EntityContainer Projectiles { get; set; }




        public Player()
        {
        
            player = new Entity(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("Assets", "Images", "Player.png")));
            
            projectile = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));
            
            Projectiles = new EntityContainer();
            
        }

        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent)
        {
            if (eventType == GameEventType.PlayerEvent)
            {
                switch (gameEvent.Parameter1)
                {
                    case "KEY_PRESS":
                        switch (gameEvent.Message)
                        {
                            case "SHOOT": 
                                Projectiles.AddDynamicEntity(
                                    new DynamicShape(player.Shape.Position.X+(player.Shape.Extent.X/2.0f), player.Shape.Position.Y+(player.Shape.Extent.Y/2.0f),
                                    0.008f , 0.027f ,
                                    0.0f , 0.01f),
                                    projectile);
                                break;
                            case "KEY_UP":
                                ((DynamicShape) player.Shape).Direction.Y = 0.002f;
                                break;
                            case "KEY_DOWN":
                                ((DynamicShape) player.Shape).Direction.Y = -0.002f;
                                break;
                            case "KEY_RIGHT":
                                ((DynamicShape) player.Shape).Direction.X = 0.002f;
                                break;
                            case "KEY_LEFT":
                                ((DynamicShape) player.Shape).Direction.X = -0.002f;
                                break;
                            default:
                                break; 
                        }

                        break;
                    case "KEY_RELEASE" :
                        switch (gameEvent.Message)
                        {
                            case "KEY_UP":
                                ((DynamicShape) player.Shape).Direction.Y = 0.0f;
                                break;
                            case "KEY_DOWN":
                                ((DynamicShape) player.Shape).Direction.Y = -0.0f;
                                break;
                            case "KEY_RIGHT":
                                ((DynamicShape) player.Shape).Direction.X = 0.0f;
                                break;
                            case "KEY_LEFT":
                                ((DynamicShape) player.Shape).Direction.X = -0.0f;
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        


        
        public void Update()
        {
            
            player.Shape.Move();
            player.RenderEntity();
            Projectiles.RenderEntities();
         }

    }
}