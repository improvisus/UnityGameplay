using Game.Bullets;
using UnityEngine;

namespace Game.Components
{
    public class WeaponComponent : MonoBehaviour
    {
        [SerializeField]
        private BulletConfig bulletConfig;

        [SerializeField]
        private Transform firePoint;
        
        public Vector2 Position => firePoint.position;
        
        private BulletPool bulletPool;

        public void Construct(BulletPool bulletPool)
        {
            this.bulletPool = bulletPool;
        }
        
        public void Fire(Vector2 direction)
        {
            bulletPool.Create(new BulletPool.Args
            {
                teamType = bulletConfig.teamType,
                physicsLayer = bulletConfig.physicsLayer,
                color = bulletConfig.color,
                damage = bulletConfig.damage,
                position = firePoint.position,
                direction = direction,
                speed = bulletConfig.speed
            });
        }
    }
}