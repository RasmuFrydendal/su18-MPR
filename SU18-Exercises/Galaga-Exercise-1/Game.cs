using System;
using System.Collections.Generic;
using System.IO;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.EventBus;
using DIKUArcade.Timers;

namespace Galaga_Exercise_1
{
    public class Game : IGameEventProcessor<object>
    {
        private Window win;

        private Entity player;
        
        private GameEventBus<object> eventBus;

        private ImageStride enemyStrides;

        private Image laser;
        
        private EntityContainer enemies;

        private EntityContainer lasers;
        
        private GameTimer gameTimer;

        private Player gamePlayer;
        
        public Game()
        {

            
            win = new Window("Galaga", 500, AspectRatio.R16X9);
            gamePlayer = new Player();
            
            laser = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));
        
            player = new Entity(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("Assets", "Images", "Player.png")));
            
            eventBus = new GameEventBus<object>();
            eventBus.InitializeEventBus(new List<GameEventType>() {
                GameEventType.InputEvent,  // key press / key release
                GameEventType.WindowEvent, // messages to the window
                GameEventType.PlayerEvent  // commands issued to the player object,
                });                        // e.g. move, destroy, receive health, etc.    
            win.RegisterEventBus(eventBus);
            eventBus.Subscribe(GameEventType.InputEvent, this);
            eventBus.Subscribe(GameEventType.WindowEvent, this );
            eventBus.Subscribe(GameEventType.PlayerEvent, gamePlayer );
            
           
            enemyStrides = new ImageStride(100,ImageStride.CreateStrides(4,
                Path.Combine("Assets", "Images", "BlueMonster.png")));
            enemies = new EntityContainer();
            lasers = new EntityContainer();
            gameTimer = new GameTimer(60,60);
            AddEnemies();
            
            
        }
        
        private void AddEnemies()
        {
            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < 8; i++)
                {
                    enemies.AddDynamicEntity(
                        (new DynamicShape(0.1f + i / 10.0f, 0.9f - j / 10.0f, 0.085f, 0.085f)), new ImageStride(100,ImageStride.CreateStrides(4,
                            Path.Combine("Assets", "Images", "BlueMonster.png"))));
                }
                
            }
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
                    player.Shape.Move();
                    player.RenderEntity();
                    enemies.RenderEntities();
                    lasers.RenderEntities();
                    win.SwapBuffers();
                }

                if (gameTimer.ShouldReset())
                {
                    win.Title = "Galaga | UPS: " +  gameTimer.CapturedUpdates +
                                ", FPS: " + gameTimer.CapturedFrames;
                }
            }


        }
        
        
        public void KeyPress(string key) {
            switch(key) {
                case "KEY_ESCAPE":
                    eventBus.RegisterEvent(
                            GameEventFactory<object>.CreateGameEventForAllProcessors(
                                GameEventType.WindowEvent, this, "CLOSE_WINDOW", "", ""));
                    win.CloseWindow();
                    break;
                case "KEY_SPACE":
                    eventBus.RegisterEvent(
                    GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.WindowEvent, this, "Shoot", "", ""));
                    
                    lasers.AddDynamicEntity(
                        new DynamicShape(player.Shape.Position.X+(player.Shape.Extent.X/2.0f), player.Shape.Position.Y+(player.Shape.Extent.Y/2.0f),
                            0.008f , 0.027f ,
                            0.0f , 0.01f),
                            laser);
                    break;
                case "KEY_UP":
                    eventBus.RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.InputEvent, this, "UP", "", ""));
                    if (player.Shape.Position.Y+0.004f >= 1.0f)
                    {
                        ((DynamicShape) player.Shape).Direction.Y = 0.000f;
                    }
                    else
                    {      
                        ((DynamicShape) player.Shape).Direction.Y = 0.004f;
                    }
                    break;
                case "KEY_DOWN":
                    if ( player.Shape.Position.Y-0.004f <= 0.0f)
                    {
                        ((DynamicShape) player.Shape).Direction.Y = 0.000f;
                    }
                    else
                    {
                        ((DynamicShape) player.Shape).Direction.Y = -0.004f;      
                    }
                    break;
                case "KEY_LEFT":
                    if (player.Shape.Position.X+0.002f > 1.0f|| player.Shape.Position.X -0.002f < 0.0f)
                    {
                        ((DynamicShape) player.Shape).Direction.X = 0.000f;
                    }
                    else
                    {
                        ((DynamicShape) player.Shape).Direction.X = -0.002f;      
                    }
                    break;
                case "KEY_RIGHT":
                    if (player.Shape.Position.X+0.002f > 1.0f|| player.Shape.Position.X-0.002f < 0.0f)
                    {
                        ((DynamicShape) player.Shape).Direction.X = 0.000f;
                    }
                    else
                    {
                        ((DynamicShape) player.Shape).Direction.X = 0.002f;      
                    }
                    break;
                
            }
                   
             // choose a fittingly small number
        }
    
        public void KeyRelease(string key)
        {
            switch (key)
            {
                case "KEY_SPACE":
                    lasers.AddDynamicEntity(new DynamicShape(0.008f , 0.027f , 0.0f , 0.01f),laser);
                    break;
                case "KEY_UP":
                    ((DynamicShape) player.Shape).Direction.Y = 0.0f;
                    break;
                case "KEY_DOWN":
                    ((DynamicShape) player.Shape).Direction.Y = 0.0f;
                    break;
                case "KEY_LEFT":
                    ((DynamicShape) player.Shape).Direction.X = 0.0f;
                    break;
                case "KEY_RIGHT":
                    ((DynamicShape) player.Shape).Direction.X = 0.0f;
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
