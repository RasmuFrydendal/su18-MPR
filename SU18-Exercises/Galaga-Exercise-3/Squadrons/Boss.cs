using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Galaga_Exercise_3.GalagaEntities;
using Galaga_Exercise_3.MovementStrategy;

namespace Galaga_Exercise_3.Squadrons {
    public class Boss : ISquadron{

        public EntityContainer<Enemy> Enemies { get; }
        public int MaxEnemies { get; } = 1;
        private Vec2F startPosition;
        private Vec2F size;
        public IMovementStrategy MovementStrategy { get; }

        public Boss() {
            Enemies = new EntityContainer<Enemy>();
            size = new Vec2F(0.3f,0.3f);
            startPosition = new Vec2F(
                0.5f-size.X/2.0f,
                1-size.X);
            MovementStrategy = new ZigZagDown();
        }

        public void Move() {
            MovementStrategy.MoveEnemies(Enemies);
        }
        
        
        public void CreateEnemies(List<Image> enemyStrides) {
            ImageStride enemystride = new ImageStride(30,enemyStrides);
            Enemies.AddDynamicEntity(new Enemy(new StationaryShape(startPosition,size),enemystride));
        }
    }
}