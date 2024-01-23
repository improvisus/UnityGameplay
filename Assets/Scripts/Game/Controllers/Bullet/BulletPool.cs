using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using Game.Common;
using Game.Pooling;
using UnityEngine;

namespace Game.Controllers.Bullet
{
    public class BulletPool : GenericPool<AtomicObject, BulletPool.Args>
    {
        public struct Args
        {
            public Transform FirePoint;
        }

        protected override AtomicObject Init(AtomicObject bullet, Args args)
        {
            var bulletDirection = bullet.GetVariable<Vector3>(ObjectAPI.MoveDirection);
            var bulletTransform = bullet.Get<Transform>(ObjectAPI.Transform);
            var resetEvent = bullet.Get<IAtomicEvent>(ObjectAPI.Reset);
            
            resetEvent?.Invoke();
            bulletTransform.position = args.FirePoint.position;
            bulletDirection.Value = args.FirePoint.forward;
            
            return bullet;
        }
    }
}