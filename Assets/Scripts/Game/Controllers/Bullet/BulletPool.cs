using Atomic.Elements;
using Atomic.Extensions;
using Game.Common;
using Game.Objects;
using Game.Pooling;
using UnityEngine;

namespace Game.Controllers
{
    public class BulletPool : GenericPool<Bullet, BulletPool.Args>
    {
        public struct Args
        {
            public Transform FirePoint;
        }

        protected override Bullet Init(Bullet bullet, Args args)
        {
            var bulletDirection = bullet.GetVariable<Vector3>(ObjectAPI.MoveDirection);
            var bulletTransform = bullet.Get<Transform>(ObjectAPI.Transform);
            var resetEvent = bullet.Get<IAtomicEvent>(ObjectAPI.Reset);
            
            resetEvent?.Invoke();
            bulletTransform.position = args.FirePoint.position;
            bulletDirection.Value = args.FirePoint.forward;
            
            return bullet;
        }

        protected override void Install(Bullet bullet)
        {
            bullet.Construct(this);
        }
    }
}