using Atomic.Elements;
using Atomic.Extensions;
using Game.Common;
using Game.Objects.Projectiles;
using Game.Pooling;
using UnityEngine;

namespace Game.Controllers
{
    public class ProjectilePool : GenericPool<Projectile, ProjectilePool.Args>
    {
        public struct Args
        {
            public Transform FirePoint;
            public int Damage;
        }

        protected override Projectile Init(Projectile projectile, Args args)
        {
            var projectileDirection = projectile.GetVariable<Vector3>(ObjectAPI.MoveDirection);
            var projectileTransform = projectile.Get<Transform>(ObjectAPI.Transform);
            var projectileDamage = projectile.GetVariable<int>(ObjectAPI.Damage);
            var resetEvent = projectile.Get<IAtomicEvent>(ObjectAPI.Reset);

            resetEvent?.Invoke();
            projectileTransform.position = args.FirePoint.position;
            projectileDirection.Value = args.FirePoint.forward;
            projectileDamage.Value = args.Damage;
            
            return projectile;
        }

        protected override void Install(Projectile projectile)
        {
            projectile.Construct(this);
        }
    }
}