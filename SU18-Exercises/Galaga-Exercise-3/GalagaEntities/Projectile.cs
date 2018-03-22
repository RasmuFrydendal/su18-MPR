using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Physics;
using Galaga_Exercise_3.Squadrons;

namespace Galaga_Exercise_3.GalagaEntities {
    public class Projectile : Entity {
        public int Damage { get; private set; }

        public Projectile(Shape shape, IBaseImage image) : base(shape, image) {
            Damage = 10;
        }


        public void DealDamage(Enemy enemy) {
            int enemyHealth = enemy.Health;
            enemy.TakeDamage(Damage);
            Damage -= enemyHealth;
            
            DeleteEntity();
            
        }
        
        private static EntityContainer<Projectile>.IteratorMethod delete = delegate(Projectile projectile) {  };
        private static EntityContainer<Enemy>.IteratorMethod deleteE = delegate(Enemy enemy) {  };

        
        public static void IterateShot(EntityContainer<Projectile> projectiles,
            List<ISquadron> squads) {
            foreach (Projectile projectile in projectiles) {
                
                projectile.Shape.Move();
                if (projectile.Shape.Position.Y > 1.0f) {
                    projectile.DeleteEntity();
                }
                
                foreach (ISquadron squad in squads) {
                    EntityContainer<Enemy> enemies = squad.Enemies;
                    foreach (Enemy enemy in enemies) {
                        CollisionData collision =
                            CollisionDetection.Aabb(((DynamicShape) projectile.Shape ),
                                enemy.Shape);
                        if (collision.Collision) {
                            projectile.DealDamage(enemy);
                        }
                        projectiles.Iterate(Projectile.delete);
                    }
                    enemies.Iterate(Projectile.deleteE);
                }
            }
        }



    }
}