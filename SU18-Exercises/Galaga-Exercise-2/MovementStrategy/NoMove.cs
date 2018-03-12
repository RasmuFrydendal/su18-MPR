using DIKUArcade.Entities;
using Galaga_Exercise_2.GalagaEntities;

namespace Galaga_Exercise_2.MovementStrategy {
    
    public class NoMove : IMovementStrategy {
    
        public void MoveEnemy(Enemy enemy) {}

        public void MoveEnemies(EntityContainer<Enemy> enemies) {}
    }
}