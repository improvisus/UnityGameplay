using Game.Bullets;
using Game.Character;
using Game.Common;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Enemy
{
    public class EnemySystemInstaller : Installer
    {
        [SerializeField]
        private CharacterService characterService;
        [SerializeField]
        private BulletPool bulletPool;
        [SerializeField]
        private EnemyPool enemyPool;
        
        [FormerlySerializedAs("enemyDeathObserver")]
        [SerializeField]
        private EnemyDeathController enemyDeathController;
        [SerializeField]
        private EnemyFireController enemyFireController;
        [SerializeField]
        private EnemySpawner enemySpawner;
        
        protected override void Install()
        {
            enemyDeathController.Construct(enemyPool);
            enemyFireController.Construct(enemyPool, bulletPool);
            enemySpawner.Construct(characterService, enemyPool);
        }
    }
}