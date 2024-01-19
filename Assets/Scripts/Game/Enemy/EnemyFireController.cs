using Game.Bullets;
using Game.Components;
using Game.Enemy.Agents;
using UnityEngine;

namespace Game.Enemy
{
    public class EnemyFireController : MonoBehaviour
    {
        private EnemyPool enemyPool;
        
        private BulletPool bulletPool;
        
        public void Construct(EnemyPool enemyPool, BulletPool bulletPool)
        {
            this.enemyPool = enemyPool;
            this.bulletPool = bulletPool;
        }
        
        private void OnEnable()
        {
            enemyPool.OnCreate += EnemyPoolOnCreate;
            enemyPool.OnRelease += EnemyPoolOnRelease;
        }

        private void OnDisable()
        {
            enemyPool.OnCreate -= EnemyPoolOnCreate;
            enemyPool.OnRelease -= EnemyPoolOnRelease;
        }

        private void EnemyPoolOnCreate(GameObject enemy)
        {
            if (enemy.TryGetComponent(out EnemyAttackAgent enemyAttackAgent))
                enemyAttackAgent.OnFire += OnFire;
        }

        private void EnemyPoolOnRelease(GameObject enemy)
        {
            if (enemy.TryGetComponent(out EnemyAttackAgent enemyAttackAgent))
                enemyAttackAgent.OnFire -= OnFire;
        }
        
        private void OnFire(GameObject enemy, GameObject target)
        {
            var weaponComponent = enemy.GetComponent<WeaponComponent>();
            var bulletConfig = weaponComponent.BulletConfig;
            
            var direction = (Vector2) target.transform.position - weaponComponent.Position;
            
            bulletPool.Create(new BulletPool.Args
            {
                teamType = bulletConfig.teamType,
                physicsLayer = bulletConfig.physicsLayer,
                color = bulletConfig.color,
                damage = bulletConfig.damage,
                position = weaponComponent.Position,
                direction = direction.normalized,
                speed = bulletConfig.speed
            });
        }
    }
}