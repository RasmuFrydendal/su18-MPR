using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Timers;
using Galaga_Exercise_2.GalagaEntities;
using Galaga_Exercise_2.MovementStrategy;

namespace Galaga_Exercise_2.Squadrons {
    public class Invasion : ISquadron{
        
        public EntityContainer<Enemy> Enemies { get; }
        public int MaxEnemies { get; }
        private GameTimer timer;
        
        public IMovementStrategy MovementStrategy { get; }
        
        public Invasion() {
            MaxEnemies = 11;
            Enemies = new EntityContainer<Enemy>(MaxEnemies);
            timer = new GameTimer(60,60);
            MovementStrategy = new ZigZagDown();
        }
                
        public void Move() {
            MovementStrategy.MoveEnemies(Enemies);
        }

        public void CreateEnemies(List<Image> enemyStrides) {
            throw new System.NotImplementedException();
        }
    }
}



















