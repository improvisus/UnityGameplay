using Game.Bullets;
using Game.Components;
using Game.Components.Rules;
using Pooling;
using UnityEngine;

namespace Game.Enemy
{
    public class EnemyPool : GenericPool<GameObject, EnemyPool.Args>
    {
        [SerializeField]
        private BulletPool bulletPool;
        
        protected override GameObject Init(GameObject enemy, Args args)
        {
            enemy.transform.position = args.spawnPosition.position;
            enemy.GetComponent<EnemyMoveRule>().SetDestination(args.attackPosition.position);
            enemy.GetComponent<EnemyAttackRule>().SetTarget(args.target);
            
            return enemy;
        }
        
        protected override void Install(GameObject bullet)
        {
            var weapon = bullet.GetComponent<WeaponComponent>();
            weapon.Construct(bulletPool);
        }
        
        public struct Args
        {
            public Transform spawnPosition;
            public Transform attackPosition;
            public GameObject target;
        }
    }
}