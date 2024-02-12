using System;
using Atomic.Elements;
using Game.Controllers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Actions
{
    [Serializable]
    public class ProjectileCreateAction : IAtomicAction
    {
        private ProjectilePool projectilePool;
        private IAtomicValue<int> projectileDamage;
        private Transform projectilePoint;
        
        public void Compose(ProjectilePool projectilePool, IAtomicValue<int> projectileDamage, Transform projectilePoint)
        {
            this.projectilePool = projectilePool;
            this.projectileDamage = projectileDamage;
            this.projectilePoint = projectilePoint;
        }
        
        [Button]
        public void Invoke()
        {
            projectilePool.Create(new ProjectilePool.Args
            {
                FirePoint = projectilePoint,
                Damage = projectileDamage.Value
            });
        }
    }
}