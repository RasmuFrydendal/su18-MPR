using System;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Galaga_Exercise_2.GalagaEntities;
using Galaga_Exercise_2.MovementStrategy;

namespace Galaga_Exercise_2.Squadrons {
    public class Squad : ISquadron {
        
        public EntityContainer<Enemy> Enemies { get; }
        public int MaxEnemies { get; }
        
        private static Vec2F size   = new Vec2F(0.08f,0.08f);
        private static float xMin   = 0.1f;        
        private static float xMax   = 0.9f-Squad.size.X;
        private static float room   = 0.02f;
        private static float center = 0.5f;
        private static int enemiesOnRow = (int)Math.Floor((Squad.xMax - Squad.xMin)/(Squad.size.X + Squad.room)-Squad.room);

        private ImageStride enemystride;

        
        public IMovementStrategy MovementStrategy { get; }
        
        public Squad() {
            MaxEnemies = 20;
            Enemies = new EntityContainer<Enemy>(MaxEnemies);
            MovementStrategy = new Down();
        }

        public void Move() {
            MovementStrategy.MoveEnemies(Enemies);
        }
        
        public void CreateEnemies(List<Image> enemyStrides) {

            List<Vec2F> positionList = PositionList();
            
            for (int i = 0; i < positionList.Count; i++) {
                enemystride = new ImageStride(60,enemyStrides);
                Vec2F position = positionList[i];    
                Enemies.AddDynamicEntity(new Enemy(new StationaryShape(position,Squad.size),enemystride));
            }
            
        }

        
        
        private List<Vec2F> PositionList() {
            List<Vec2F> list = new List<Vec2F>();

            float direction = 1.0f;
            float offset = 0.0f;
            int line = 1;
            
            

            
            if (Squad.enemiesOnRow % 2 == 0) {
                offset = -(Squad.room / 2);
            } 
            else {
                offset = -(Squad.size.X / 2);
            }


            while (line*Squad.enemiesOnRow < MaxEnemies+Squad.enemiesOnRow) {
                
                    for (int i = 1; i <= Squad.enemiesOnRow; i++) {
                        float xPos = (Squad.center + (float)Math.Floor(i/2.0f) * (Squad.size.X + Squad.room) * direction) + offset;
                        float yPos = 1.0f-line*(Squad.size.Y+Squad.room);
                        
                        list.Add(new Vec2F(xPos,yPos));
                        
                        direction *= -1;
                    }

                line += 1;
            }

            
            return list;
        }

    }
}