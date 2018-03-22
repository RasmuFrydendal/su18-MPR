using System;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using Galaga_Exercise_3.GalagaEntities;

namespace Galaga_Exercise_3.MovementStrategy {
    public class ZigZagDown : IMovementStrategy {
        
        private float s = 0.0003f;
        private float p = 0.045f;
        private float a = 0.05f;
        
        public void MoveEnemy(Enemy enemy) {
            Vec2F pos = enemy.Shape.Position;
            Vec2F sPos = enemy.StartPos;
            pos.Y -= s;
            pos.X = sPos.X + a * (float) Math.Sin((2 * Math.PI * (sPos.Y - pos.Y)/p));
        }

        public void MoveEnemies(EntityContainer<Enemy> enemies) {
            foreach (Enemy enemy in enemies) {
                MoveEnemy(enemy);
            }
        }
    }
}