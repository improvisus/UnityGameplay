using Game.Components;
using Game.Components.Rules;
using UnityEngine;

namespace Game.Bullets
{
    public class BulletDealDamageController : MonoBehaviour
    {
        private BulletPool bulletPool;
        
        public void Construct(BulletPool bulletPool)
        {
            this.bulletPool = bulletPool;
        }
        
        void OnEnable()
        {
            bulletPool.OnCreate += BulletFactoryOnCreate;
            bulletPool.OnRelease += BulletFactoryOnRelease;
        }

        void OnDisable()
        {
            bulletPool.OnCreate -= BulletFactoryOnCreate;
            bulletPool.OnRelease -= BulletFactoryOnRelease;
        }

        private void BulletFactoryOnCreate(GameObject bullet)
        {
            if(bullet.TryGetComponent(out CollisionComponent collisionComponent))
                collisionComponent.OnCollisionEntered += OnBulletCollision;
        }

        private void BulletFactoryOnRelease(GameObject bullet)
        {
            if(bullet.TryGetComponent(out CollisionComponent collisionComponent))
                collisionComponent.OnCollisionEntered -= OnBulletCollision;
        }

        private void OnBulletCollision(GameObject bullet, Collision2D collision)
        {
            if (!bullet.TryGetComponent(out BulletCollisionRule bulletCollisionRule))
                return;
            
            var other = collision.gameObject;
            bulletCollisionRule.Collision(other);

            bulletPool.Release(bullet);
        }
    }
}