using DIKUArcade.Entities;
using Galaga_Exercise_3.GalagaEntities;

namespace Galaga_Exercise_3.MovementStrategy {
    
    public class NoMove : IMovementStrategy {
    
        public void MoveEnemy(Enemy enemy) {}

        public void MoveEnemies(EntityContainer<Enemy> enemies) {}
    }
}