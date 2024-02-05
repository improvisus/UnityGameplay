using Atomic.Behaviours;
using Game.Controllers;

namespace Game.Objects.Projectiles
{
    public abstract class Projectile : AtomicBehaviour
    {
        protected ProjectilePool projectilePool;
        
        public void Construct(ProjectilePool projectilePool)
        {
            this.projectilePool = projectilePool;
        }
    }
}