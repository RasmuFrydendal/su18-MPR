using DIKUArcade.Entities;
using Galaga_Exercise_2.GalagaEntities;

namespace Galaga_Exercise_2.MovementStrategy {
    public class Down : IMovementStrategy {
        private float s = 0.0006f;
        
        public void MoveEnemy(Enemy enemy) {
            enemy.Shape.Position.Y -= s;
        }

        public void MoveEnemies(EntityContainer<Enemy> enemies) {
            if (enemies!=null) {
                foreach (Enemy enemy in enemies) {
                    MoveEnemy(enemy);
                }
            }
        }
    }
}