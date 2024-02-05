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
        private Transform projectilePoint;
        
        public void Compose(ProjectilePool projectilePool, Transform projectilePoint)
        {
            this.projectilePool = projectilePool;
            this.projectilePoint = projectilePoint;
        }
        
        [Button]
        public void Invoke()
        {
            projectilePool.Create(new ProjectilePool.Args
            {
                FirePoint = projectilePoint
            });
        }
    }
}