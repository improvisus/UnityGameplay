using Game.Common;
using UnityEngine;

namespace Game.Bullets
{
    public class BulletSystemInstaller : Installer
    {
        [SerializeField]
        private BulletPool bulletPool;
        
        [SerializeField]
        private BulletDealDamageController bulletDealDamageController;
        [SerializeField]
        private BulletLevelBoundsController bulletLevelBoundsController;
        
        protected override void Install()
        {
            bulletDealDamageController.Construct(bulletPool);
            bulletLevelBoundsController.Construct(bulletPool);
        }
    }
}