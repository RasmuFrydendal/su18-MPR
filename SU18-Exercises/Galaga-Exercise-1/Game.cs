using System;
using System.Collections.Generic;
using System.IO;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.EventBus;
using DIKUArcade.Physics;
using DIKUArcade.Timers;

namespace Galaga_Exercise_1
{
    public class Game : IGameEventProcessor<object>
    {
        private Window win;
        
        private GameEventBus<object> eventBus;


        private ImageStride enemyStrides;
        
        private EntityContainer enemies;
            
        private Player player;
        
        private GameTimer gameTimer;

        
        private EntityContainer.IteratorMethod projtIteratorMethod;

        private EntityContainer.IteratorMethod destoryIterator;

        private List<Image> explosionStrides;
        private AnimationContainer explosions;
        
        private int explosionLength = 500;
        
        public Game()
        {

            win = new Window("Galaga", 500, AspectRatio.R16X9);
           
            player = new Player();
            
            enemies= new EntityContainer();

            projtIteratorMethod = ProjectileIterator;

            destoryIterator = DestoryIterator;

            enemyStrides = new ImageStride(80, ImageStride.CreateStrides(4,
                Path.Combine("Assets", "Images", "BlueMonster.png")));

            explosionStrides = ImageStride.CreateStrides(8, 
                Path.Combine("Assets","Images","Explosion.png"));
            
            explosions = new AnimationContainer(10);
            
            AddEnemies();
            
            eventBus = new GameEventBus<object>();
            eventBus.InitializeEventBus(new List<GameEventType>() {
                GameEventType.InputEvent,  // key press / key release
                GameEventType.WindowEvent, // messages to the window
                GameEventType.PlayerEvent  // commands issued to the player object,
                });                        // e.g. move, destroy, receive health, etc.    
            win.RegisterEventBus(eventBus);
            eventBus.Subscribe(GameEventType.InputEvent, this);
            eventBus.Subscribe(GameEventType.WindowEvent, this );
            eventBus.Subscribe(GameEventType.PlayerEvent, player );
            
            gameTimer = new GameTimer(60,60);

        }
        
        public void GameLoop()
        {
            while (win.IsRunning())
            {
                
                //Timer
                gameTimer.MeasureTime();
                
                
                //EventUpdate
                while (gameTimer.ShouldUpdate())
                {
                    win.PollEvents();
                    eventBus.ProcessEvents();
                }

                
                //Render
                if (gameTimer.ShouldRender())
                {
                    win.Clear();
                    
                    
                    IterateShot();
                    
                    enemies.RenderEntities();
                    
                    explosions.RenderAnimations();
                    
                    
                    player.Update();
                    
                    win.SwapBuffers();
                }

                if (gameTimer.ShouldReset())
                {
                    win.Title = "Galaga | UPS: " +  gameTimer.CapturedUpdates +
                                ", FPS: " + gameTimer.CapturedFrames;
                }
            }
        }
        
        
        private void AddEnemies()
        {
            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < 8; i++)
                {
                    enemies.AddDynamicEntity(
                        (new DynamicShape(0.1f + i / 10.0f, 0.9f - j / 10.0f, 0.1f, 0.1f)), enemyStrides);
                }
                
            }
        }

        private void ProjectileIterator(Entity entity)
        {
            entity.Shape.Move();
            
            if (entity.Shape.Position.Y > 1.0f)
            {
                entity.DeleteEntity();
            }
            
            foreach (Entity enemy in enemies)
            {
                CollisionData collision = CollisionDetection.Aabb((DynamicShape) entity.Shape, enemy.Shape);
                if (collision.Collision)
                {
                  entity.DeleteEntity();
                  enemy.DeleteEntity();
                  AddExplosion(enemy.Shape.Position, enemy.Shape.Extent);
                }
            }
            enemies.Iterate(destoryIterator);    
        }

       
        
        private void DestoryIterator (Entity entity)
        {
            
        }

        public void IterateShot()
        {
            player.Projectiles.Iterate(projtIteratorMethod);
        }


        public void AddExplosion(float posX, float posY,
            float extentX, float extentY) {
            explosions.AddAnimation(
                new StationaryShape(posX, posY, extentX, extentY), explosionLength,
                new ImageStride(explosionLength / 8, explosionStrides));
        }
        
        public void AddExplosion(Vec2F pos, Vec2F extent){AddExplosion(pos.X,pos.Y,extent.X,extent.Y);}

        public void KeyPress(string key) {
            switch(key) {
                case "KEY_ESCAPE":
                        eventBus.RegisterEvent(
                                GameEventFactory<object>.CreateGameEventForAllProcessors(
                                    GameEventType.WindowEvent, this, "CLOSE_WINDOW", "KEY_PRESS", ""));
                    break;
                
                case "KEY_SPACE":
                    eventBus.RegisterEvent(
                    GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.PlayerEvent, this, "SHOOT", "KEY_PRESS", ""));
                    break;
                case "KEY_UP":
                    eventBus.RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.PlayerEvent, this, "KEY_UP", "KEY_PRESS", ""));
                    break;
                case "KEY_DOWN":
                    eventBus.RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.PlayerEvent, this, "KEY_DOWN", "KEY_PRESS", ""));
                    break;
                case "KEY_LEFT":
                    eventBus.RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.PlayerEvent, this, "KEY_LEFT", "KEY_PRESS", ""));
                    break;
                case "KEY_RIGHT":
                    eventBus.RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.PlayerEvent, this, "KEY_RIGHT", "KEY_PRESS", ""));
                    break;
                
            }
                   
             // choose a fittingly small number
        }
    
        public void KeyRelease(string key)
        {
            switch (key)
            {    
                case "KEY_UP":
                    eventBus.RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.PlayerEvent, this, "KEY_UP", "KEY_RELEASE", ""));
                    break;
                case "KEY_DOWN":
                    eventBus.RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.PlayerEvent, this, "KEY_DOWN", "KEY_RELEASE", ""));
                    break;
                case "KEY_LEFT":
                    eventBus.RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.PlayerEvent, this, "KEY_LEFT", "KEY_RELEASE", ""));
                    break; 
                case "KEY_RIGHT":
                    eventBus.RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.PlayerEvent, this, "KEY_RIGHT", "KEY_RELEASE", ""));
                    break;
                default: 
                    break;
            }
               
                    
        }
        
        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent) {
            if (eventType == GameEventType.WindowEvent) {
                switch (gameEvent.Message) {
                    case "CLOSE_WINDOW":
                        if (gameEvent.Parameter1 == "KEY_PRESS")
                        {
                            win.CloseWindow();
                        }
                        break;
                    default:
                        break;
                }
            } else if (eventType == GameEventType.InputEvent) {
                switch (gameEvent.Parameter1) {
                    case "KEY_PRESS":
                        KeyPress(gameEvent.Message);
                        break;
                    case "KEY_RELEASE":
                        KeyRelease(gameEvent.Message);
                        break;
                }
            } 
        }
    }
}
