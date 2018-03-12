using System;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Physics;
using Galaga_Exercise_2.Squadrons;

namespace Galaga_Exercise_2.GalagaEntities {
    public class Projectile : Entity {
        public int Damage { get; private set; }

        public Projectile(Shape shape, IBaseImage image) : base(shape, image) {
            Damage = 10;
        }


        public void DealDamage(Enemy enemy) {
            int enemyHealth = enemy.Health;
            enemy.TakeDamage(Damage);
            Damage -= enemyHealth;
            if (Damage <= 0) {
                DeleteEntity();
            }
        }
        
        private static EntityContainer<Enemy>.IteratorMethod delete = delegate(Enemy enemy) {  };

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
                            break;
                        }
                    enemies.Iterate(Projectile.delete);
                    }
                }
            }
        }



    }
}