using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Galaga_Exercise_2.GalagaEntities;

namespace Galaga_Exercise_2.Squadrons {
    public interface ISquadron  {
        
        EntityContainer<Enemy> Enemies { get; }
        
        int MaxEnemies { get; }

        void Move();

        void CreateEnemies(List<Image> enemyStrides);
        
    }
}