using System;
using System.IO;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga_Exercise_2.GalagaEntities
{
    public class Player : IGameEventProcessor<object>
    {
        
        //Player
        private Entity player;
        private Vec2F movementSpeed;
        
        //Projectiles
        private Image projectile;
        public EntityContainer<Projectile> Projectiles { get; set; }

        


        public Player()
        {
            
            movementSpeed = new Vec2F(0.004f,0.004f);
            player = new Entity(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("Assets", "Images", "Player.png")));
            
            
            projectile = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));
            
            Projectiles = new EntityContainer<Projectile>();
            
        }

        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent) {
            
            if (eventType != GameEventType.PlayerEvent) {
                return;
            }

            switch (gameEvent.Parameter1)
            {
            case "KEY_PRESS":
                switch (gameEvent.Message)
                {
                case "SHOOT": 
                    Projectiles.AddDynamicEntity
                    (new Projectile
                        (new DynamicShape
                            (player.Shape.Position.X+(player.Shape.Extent.X/2.0f), 
                                player.Shape.Position.Y+(player.Shape.Extent.Y/2.0f),
                                0.008f , 0.027f,
                                0.0f , 0.01f),
                            projectile)
                    );
                                    
                    break;
                case "KEY_UP":
                    ((DynamicShape) player.Shape).Direction.Y = movementSpeed.Y;
                    break;
                case "KEY_DOWN":
                    ((DynamicShape) player.Shape).Direction.Y = -movementSpeed.Y;
                    break;
                case "KEY_RIGHT":
                    ((DynamicShape) player.Shape).Direction.X = movementSpeed.X;
                    break;
                case "KEY_LEFT":
                    ((DynamicShape) player.Shape).Direction.X = -movementSpeed.X;
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


        public Tuple<bool,bool> WithinBounds() {
            bool withinBoundsX = 
                !(player.Shape.Position.X >= 0.9f && 
                  ((DynamicShape)player.Shape).Direction.X > 0.0f);
            bool withinBoundsY =
                !(player.Shape.Position.Y >= 0.9f && 
                  ((DynamicShape)player.Shape).Direction.Y > 0.0f);
            return new Tuple<bool,bool>(withinBoundsX,withinBoundsY);
            
        }

        
        public void Update()
        {
            if (WithinBounds().Item1 && WithinBounds().Item2) {
                player.Shape.Move();
            }
            player.RenderEntity();
            Projectiles.RenderEntities();
         }

    }
}