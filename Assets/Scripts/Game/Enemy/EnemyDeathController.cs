using Game.Components;
using UnityEngine;

namespace Game.Enemy
{
    public class EnemyDeathController : MonoBehaviour
    {
        private EnemyPool enemyPool;

        public void Construct(EnemyPool enemyPool)
        {
            this.enemyPool = enemyPool;
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
            enemy.GetComponent<HitPointsComponent>().OnHitPointEmpty += OnEnemyDeath;
        }
        
        private void EnemyPoolOnRelease(GameObject enemy)
        {
            enemy.GetComponent<HitPointsComponent>().OnHitPointEmpty -= OnEnemyDeath;
        }

        private void OnEnemyDeath(GameObject enemy)
        {
            enemy.GetComponent<HitPointsComponent>().OnHitPointEmpty -= OnEnemyDeath;
            enemyPool.Release(enemy);
        }
    }
}