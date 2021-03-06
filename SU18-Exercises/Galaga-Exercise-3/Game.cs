﻿using System.Collections.Generic;
using DIKUArcade;
using DIKUArcade.EventBus;
using DIKUArcade.Timers;
using Galaga_Exercise_3.GalagaState;

namespace Galaga_Exercise_3
{
    public class Game : IGameEventProcessor<object>
    {
        private Window win;
        
        private GameEventBus<object> eventBus;

        private StateMachine state;
        
        private GameTimer gameTimer;
//        
//        //Enemies
//        private  List<ISquadron> squadronContainer;
//        //Squadrons
//        private Squad squad;
//        private Boss boss;
//        private Invasion invasion;
        
//        //Movement
//        private Down down;
//        
//        
//        //Player
//        private Player player;
//        
        //Timer

//        //Explosions
//        private List<Image> explosionStrides;
//        private AnimationContainer explosions;
//        private int explosionLength = 500;
//        
        
        public Game()
        {

            win = new Window("Galaga", 500, AspectRatio.R16X9);
            state = new StateMachine();
            
            //Player
//            player = new Player();
//            
            
//            //Enemies
//            squadronContainer = new List<ISquadron>();
//            
//              //Squadrons
//            squad = new Squad();
//            squad.CreateEnemies(ImageStride.CreateStrides(4,
//                Path.Combine("Assets", "Images", "BlueMonster.png")));
//            
//            boss = new Boss();
//            boss.CreateEnemies(ImageStride.CreateStrides(4,
//                Path.Combine("Assets", "Images", "BlueMonster.png")));
                
//            invasion = new Invasion();
//            invasion.CreateEnemies(ImageStride.CreateStrides(4,
//                Path.Combine("Assets", "Images", "BlueMonster.png")));

            
//            //Add to enemies
//            squadronContainer.Add(squad); 
//            squadronContainer.Add(boss);
////            squadronContainer.Add(invasion);
            
            
            //Explosiom
//            explosionStrides = ImageStride.CreateStrides(8, 
//                Path.Combine("Assets","Images","Explosion.png"));
//            
//            explosions = new AnimationContainer(10);
//            
//            //Movement
//            down = new Down();

            //Eventbus
            
            
            eventBus = new GameEventBus<object>();
            eventBus.InitializeEventBus(new List<GameEventType>() {
                GameEventType.InputEvent,  // key press / key release
                GameEventType.WindowEvent, // messages to the window
                GameEventType.PlayerEvent  // commands issued to the player object,
                });                        // e.g. move, destroy, receive health, etc.    
            win.RegisterEventBus(eventBus);
            eventBus.Subscribe(GameEventType.InputEvent, this);
            eventBus.Subscribe(GameEventType.WindowEvent, this );
            
            
            //Gametimer
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
                    state.ActiveState.UpdateGameLogic();
                }


                //Render
                if (gameTimer.ShouldRender())
                {
                    win.Clear();
                    
                    state.ActiveState.RenderState();
                    
//                    foreach (var squadron in squadronContainer) {
//                        squadron.Move();
//                        squadron.Enemies.RenderEntities();    
//                    }
//
//                    Projectile.IterateShot(player.Projectiles,squadronContainer);
//                    
//                    explosions.RenderAnimations();
//                    
//                    player.Update();
                    
                    win.SwapBuffers();
                }

                if (gameTimer.ShouldReset())
                {
                    win.Title = "Galaga | UPS: " +  gameTimer.CapturedUpdates +
                                ", FPS: " + gameTimer.CapturedFrames;
                }
            }
        }

        
        
        
//        //Explosions
//        public void AddExplosion(float posX, float posY,
//            float extentX, float extentY) {
//            explosions.AddAnimation(
//                new StationaryShape(posX, posY, extentX, extentY), explosionLength,
//                new ImageStride(explosionLength / 8, explosionStrides));
//        }
//
//        public void AddExplosion(Vec2F pos, Vec2F extent) {
//            AddExplosion(pos.X,pos.Y,extent.X,extent.Y);
//        }

        
        
        //Events
        
        public void KeyPress(string key) {
            switch(key) {
                case "KEY_ESCAPE":
                        eventBus.RegisterEvent(
                                GameEventFactory<object>.CreateGameEventForAllProcessors(
                                    GameEventType.WindowEvent, this, "CLOSE_WINDOW", "KEY_PRESS", ""));
                    break;
                default:
                    state.ActiveState.HandleKeyEvent(key,"KEY_PRESS");
                    break;

                    
                    
                    
//                case "KEY_SPACE":
//                    eventBus.RegisterEvent(
//                    GameEventFactory<object>.CreateGameEventForAllProcessors(
//                        GameEventType.PlayerEvent, this, "SHOOT", "KEY_PRESS", ""));
//                    break;
//                case "KEY_UP":
//                    eventBus.RegisterEvent(
//                        GameEventFactory<object>.CreateGameEventForAllProcessors(
//                            GameEventType.PlayerEvent, this, "KEY_UP", "KEY_PRESS", ""));
//                    break;
//                case "KEY_DOWN":
//                    eventBus.RegisterEvent(
//                        GameEventFactory<object>.CreateGameEventForAllProcessors(
//                            GameEventType.PlayerEvent, this, "KEY_DOWN", "KEY_PRESS", ""));
//                    break;
//                case "KEY_LEFT":
//                    eventBus.RegisterEvent(
//                        GameEventFactory<object>.CreateGameEventForAllProcessors(
//                            GameEventType.PlayerEvent, this, "KEY_LEFT", "KEY_PRESS", ""));
//                    break;
//                case "KEY_RIGHT":
//                    eventBus.RegisterEvent(
//                        GameEventFactory<object>.CreateGameEventForAllProcessors(
//                            GameEventType.PlayerEvent, this, "KEY_RIGHT", "KEY_PRESS", ""));
//                    break;
                
            }
        }
    
        public void KeyRelease(string key)
        {
//            switch (key)
//            {    
//                case "KEY_UP":
//                    eventBus.RegisterEvent(
//                        GameEventFactory<object>.CreateGameEventForAllProcessors(
//                            GameEventType.PlayerEvent, this, "KEY_UP", "KEY_RELEASE", ""));
//                    break;
//                case "KEY_DOWN":
//                    eventBus.RegisterEvent(
//                        GameEventFactory<object>.CreateGameEventForAllProcessors(
//                            GameEventType.PlayerEvent, this, "KEY_DOWN", "KEY_RELEASE", ""));
//                    break;
//                case "KEY_LEFT":
//                    eventBus.RegisterEvent(
//                        GameEventFactory<object>.CreateGameEventForAllProcessors(
//                            GameEventType.PlayerEvent, this, "KEY_LEFT", "KEY_RELEASE", ""));
//                    break; 
//                case "KEY_RIGHT":
//                    eventBus.RegisterEvent(
//                        GameEventFactory<object>.CreateGameEventForAllProcessors(
//                            GameEventType.PlayerEvent, this, "KEY_RIGHT", "KEY_RELEASE", ""));
//                    break;
//                default: 
//                    break;
//            }
               
                    
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
                state.ActiveState.HandleKeyEvent(gameEvent.Message, gameEvent.Parameter1);
            } else if (eventType == GameEventType.GameStateEvent) {
                state.ProcessEvent(eventType,gameEvent);
            }
            
        }
    }
}
