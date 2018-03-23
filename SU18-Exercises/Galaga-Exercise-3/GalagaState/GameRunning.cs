using System.Collections.Generic;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.State;
using Galaga_Exercise_3.GalagaEntities;
using Galaga_Exercise_3.MovementStrategy;
using Galaga_Exercise_3.Squadrons;

namespace Galaga_Exercise_3.GalagaState {
    public class GameRunning : IGameState {

        private static GameRunning instance = null;

        //Enemies
        private  List<ISquadron> squadronContainer;
        //Squadrons
        private Squad squad;
        private Boss boss;
        private Invasion invasion;
        
        //Movement
        private Down down;

        //Player
        private Player player;

        //Explosions
        private List<Image> explosionStrides;
        private AnimationContainer explosions;
        private int explosionLength = 500;

        private GameEventBus<object> eventBus;
        
        
        public GameRunning() {
            InitializeGameState();
        }
        
        public void GameLoop() {
            ;
        }

        public void InitializeGameState() {
            
            //Player
            player = new Player();

            //Enemies
            squadronContainer = new List<ISquadron>();
            
            //Squadrons
            squad = new Squad();
            squad.CreateEnemies(ImageStride.CreateStrides(4,
                Path.Combine("Assets", "Images", "BlueMonster.png")));
            
            boss = new Boss();
            boss.CreateEnemies(ImageStride.CreateStrides(4,
                Path.Combine("Assets", "Images", "BlueMonster.png")));

            
            //Add to enemies
            squadronContainer.Add(squad); 
            squadronContainer.Add(boss);
//            squadronContainer.Add(invasion);
            
            explosionStrides = ImageStride.CreateStrides(8, 
                Path.Combine("Assets","Images","Explosion.png"));
            
            explosions = new AnimationContainer(10);
            
            //Movement
            down = new Down();

            eventBus = GalagaBus.GetBus();
            eventBus.Subscribe(GameEventType.PlayerEvent, player );
            
            
        }

        public void UpdateGameLogic() {
        }

        public void RenderState() {
            
            foreach (var squadron in squadronContainer) {
                squadron.Move();
                squadron.Enemies.RenderEntities();    
            }

            Projectile.IterateShot(player.Projectiles,squadronContainer);
                    
            explosions.RenderAnimations();
                    
            player.Update();
            
        }
        
        //Explosions
        public void AddExplosion(float posX, float posY,
            float extentX, float extentY) {
            explosions.AddAnimation(
                new StationaryShape(posX, posY, extentX, extentY), explosionLength,
                new ImageStride(explosionLength / 8, explosionStrides));
        }

        public void AddExplosion(Vec2F pos, Vec2F extent) {
            AddExplosion(pos.X,pos.Y,extent.X,extent.Y);
        }
        
        
        
        

        public void HandleKeyEvent(string keyValue, string keyAction) {
            
        }

        public static IGameState GetInstance() {
            return GameRunning.instance ?? new GameRunning();
        }
    }
}
