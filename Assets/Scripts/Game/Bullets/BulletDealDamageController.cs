using Game.Components;
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
            var other = collision.gameObject;
            if (!other.TryGetComponent(out TeamComponent team))
                return;

            var teamBullet = bullet.GetComponent<TeamComponent>();
            if(teamBullet.Type == team.Type)
                return;

            if (other.TryGetComponent(out HitPointsComponent hitPoints))
            {
                var damageComponent = bullet.GetComponent<DamageComponent>();
                hitPoints.TakeDamage(damageComponent.Damage);
            }
            bulletPool.Release(bullet);
        }
    }
}