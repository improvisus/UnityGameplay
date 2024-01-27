using Game.Character;
using Game.Common;
using UnityEngine;

namespace Game.Enemy
{
    public class EnemySystemInstaller : Installer
    {
        [SerializeField]
        private CharacterService characterService;
        [SerializeField]
        private EnemyPool enemyPool;
        
        [SerializeField]
        private EnemyDeathController enemyDeathController;
        [SerializeField]
        private EnemySpawner enemySpawner;
        
        protected override void Install()
        {
            enemyDeathController.Construct(enemyPool);
            enemySpawner.Construct(characterService, enemyPool);
        }
    }
}