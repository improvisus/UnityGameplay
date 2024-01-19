using Game.Enemy.Agents;
using Pooling;
using UnityEngine;

namespace Game.Enemy
{
    public class EnemyPool : GenericPool<GameObject, EnemyPool.Args>
    {
        public struct Args
        {
            public Transform spawnPosition;
            public Transform attackPosition;
            public GameObject target;
        }
        
        protected override GameObject Init(GameObject enemy, Args args)
        {
            enemy.transform.position = args.spawnPosition.position;
            enemy.GetComponent<EnemyMoveAgent>().SetDestination(args.attackPosition.position);
            enemy.GetComponent<EnemyAttackAgent>().SetTarget(args.target);
            
            return enemy;
        }
    }
}