using System.Collections;
using Game.Character;
using UnityEngine;

namespace Game.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private EnemyPositions enemyPositions;

        private CharacterService characterService;
        private EnemyPool enemyPool;
        
        public void Construct(CharacterService characterService, EnemyPool enemyPool)
        {
            this.characterService = characterService;
            this.enemyPool = enemyPool;
        }
        
        private IEnumerator Start()
        {
            yield return SpawnProcess();
        }
        
        private IEnumerator SpawnProcess()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                enemyPool.Create(new EnemyPool.Args
                {
                    attackPosition = enemyPositions.RandomAttackPosition(),
                    spawnPosition = enemyPositions.RandomSpawnPosition(),
                    target = characterService.Character
                });
            }
        }

    }
}