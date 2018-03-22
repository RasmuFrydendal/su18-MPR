using System;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Timers;
using Galaga_Exercise_2.GalagaEntities;
using Galaga_Exercise_2.MovementStrategy;

namespace Galaga_Exercise_2.Squadrons {
    public class Invasion : ISquadron{
        
        public EntityContainer<Enemy> Enemies { get; }
        public int MaxEnemies { get; }
        private GameTimer timer;
        
        public IMovementStrategy MovementStrategy { get; }

        private static Vec2F size = new Vec2F(0.08f, 0.08f);
        private static float xMin = 0.1f;
        private static float xMax = 0.9f - Invasion.size.X;
        private static float room = 0.02f;
        private static float leftAlign = 0.1f;
        
        private ImageStride enemystride;
        
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

            List<Vec2F> positionList = PositionList();
            
            for (int i = 0; i < positionList.Count; i++) {
                enemystride = new ImageStride(60,enemyStrides);
                Vec2F position = positionList[i];
                Enemies.AddDynamicEntity(new Enemy(new StationaryShape(position,Invasion.size),enemystride));
            }
        }

        private List<Vec2F> PositionList() {
            List<Vec2F> list = new List<Vec2F>();
            
            List<int> enemiesPerRow = new List<int>(3){2,7,2};
            
            float direction = 1.0f;
            float offset = 0.0f;
            int line = 1;

            for (int j = 0; j < enemiesPerRow.Count; j++) {
                
                if (enemiesPerRow[j] % 2 == 0) {
                    offset = -(Invasion.room / 2);
                } 
                else {
                    offset = -(Invasion.size.X / 2);
                }

                for (int i = 1; i <= enemiesPerRow[j]; i++) {

                    float xPos = Invasion.leftAlign + i * (Invasion.size.X + Invasion.room);
                    float yPos = 1.0f - line * (Invasion.size.Y + Invasion.room);

                    list.Add(new Vec2F(xPos, yPos));

                }
                line += 1;
            }
            return list;

        }
    }
}



















