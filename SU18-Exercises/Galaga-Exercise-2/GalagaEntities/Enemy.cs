using System;
using System.Security.Cryptography.X509Certificates;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga_Exercise_2.GalagaEntities {
    public class Enemy : Entity {
        public Vec2F StartPos { get; }
        public int Health { get; private set; }
        public int Damage { get; }

        public Enemy(StationaryShape shape, IBaseImage image) : this(shape, image,
            30, 30) { }

        public Enemy(StationaryShape shape, IBaseImage image, int health,
            int damage) : base(shape, image) {
            Health = health;
            Damage = damage;
            StartPos = shape.Position.Copy();
        }

        public void TakeDamage(int dmg) {
            Health -= dmg;
            if (Health <= 0) {
                DeleteEntity();
            }
        }

        public int DealDamage() {
            return Damage;
        }
    }
}