using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Timers;
using Galaga_Exercise_2.GalagaEntities;

namespace Galaga_Exercise_2.Squadrons {
    public class Zerg : ISquadron{
        
        public EntityContainer<Enemy> Enemies { get; }
        public int MaxEnemies { get; }
        private GameTimer timer;
        
        public Zerg() {
            timer = new GameTimer(60,60);
            Enemies = new EntityContainer<Enemy>();
            MaxEnemies = 30;
        }
                
        public void Move() {
            
        }

        public void CreateEnemies(List<Image> enemyStrides) {
            throw new System.NotImplementedException();
        }
    }
}