using System;
using Atomic.Elements;
using Game.Controllers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Actions
{
    [Serializable]
    public class FireBulletAction : IAtomicAction
    {
        private BulletPool bulletPool;
        private Transform firePoint;
        private IAtomicValue<bool> fireCondition;
        private IAtomicEvent fireEvent;
        
        public void Compose(BulletPool bulletPool, Transform firePoint, IAtomicValue<bool> fireCondition, IAtomicEvent fireEvent)
        {
            this.bulletPool = bulletPool;
            this.firePoint = firePoint;
            this.fireCondition = fireCondition;
            this.fireEvent = fireEvent;
        }
        
        [Button]
        public void Invoke()
        {
            if (!fireCondition.Value)
                return;
            
            bulletPool.Create(new BulletPool.Args
            {
                FirePoint = firePoint
            });
            fireEvent?.Invoke();
        }
    }
}