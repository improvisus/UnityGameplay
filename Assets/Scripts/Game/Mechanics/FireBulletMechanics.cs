using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using Game.Common;
using UnityEngine;

namespace Game.Mechanics
{
    public class FireBulletMechanics
    {
        private readonly AtomicObject bulletPrefab;
        private readonly Transform firePoint;
        private readonly IAtomicEvent fireEvent;
        
        public FireBulletMechanics(AtomicObject bulletPrefab, Transform firePoint, IAtomicEvent fireEvent)
        {
            this.bulletPrefab = bulletPrefab;
            this.firePoint = firePoint;
            this.fireEvent = fireEvent;
        }

        public void OnEnable()
        {
            fireEvent.Subscribe(OnFire);
        }

        public void OnDisable()
        {
            fireEvent.Unsubscribe(OnFire);
        }
        
        private void OnFire()
        {
            var bullet = Object.Instantiate(bulletPrefab, firePoint.position, Quaternion.identity, null);
            IAtomicVariable<Vector3> bulletDirection = bullet.GetVariable<Vector3>(ObjectAPI.MoveDirection);
            
            if (bulletDirection != null)
                bulletDirection.Value = firePoint.forward;
        }
    }
}