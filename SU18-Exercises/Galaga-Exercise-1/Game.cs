using System;
using System.Collections.Generic;
using System.IO;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.EventBus;

namespace Galaga_Exercise_1
{
    public class Game
    {
        private Window win;

        private Entity player;
        
        private GameEventBus<object> eventBus;
        
        private List<Image> enemyStrides;
        private EntityContainer enemies;
        
        public Game()
        {

            
            win = new Window("Galaga", 500, AspectRatio.R1X1);
        
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
            eventBus.Subscribe(GameEventType.WindowEvent, this);
           
            enemyStrides = ImageStride.CreateStrides(4,
                Path.Combine("Assets", "Images", "BlueMonster.png"));
            enemies = new EntityContainer();
            
        }
        
        private void AddEnemies() {
            // create the desired number of enemies here. Remember:
            // - normalised coordinates
            // - add them to the entity containerS
        }



        public void GameLoop()
        {
            while (win.IsRunning())
            {
                eventBus.ProcessEvents();
                win.PollEvents();
                win.Clear();
                
                player.RenderEntity();
                
                win.SwapBuffers();
                
                player.Shape.Move();
                player.RenderEntity();
                
            }


        }
        
        
        public void KeyPress(string key) {
            switch(key) {
                case "KEY_ESCAPE":
                    eventBus.RegisterEvent(
                            GameEventFactory<object>.CreateGameEventForAllProcessors(
                                GameEventType.WindowEvent, this, "CLOSE_WINDOW", "", ""));
                    break;
                      
            }    // match on e.g. "KEY_UP", "KEY_1", "KEY_A", etc.
                   // TODO: use this method to start moving your player object
        player.Shape.Direction.X = 0.0001f; // choose a fittingly small number
        }

        public void KeyRelease(string key)
        {
            // match on e.g. "KEY_UP", "KEY_1", "KEY_A", etc.
            player.Shape.MoveX(0.0f);
            
        }
        
        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent) {
            if (eventType == GameEventType.WindowEvent) {
                switch (gameEvent.Message) {
                    case "CLOSE_WINDOW":
                        win.CloseWindow();
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
