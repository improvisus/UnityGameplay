using Game.Level;
using UnityEngine;

namespace Game.Bullets
{
    public class BulletLevelBoundsController : MonoBehaviour
    {
        [SerializeField]
        private LevelBounds levelBounds;
        
        private BulletsService bulletsService;
        private BulletPool bulletPool;
        
        public void Construct(BulletsService bulletsService, BulletPool bulletPool)
        {
            this.bulletsService = bulletsService;
            this.bulletPool = bulletPool;
        }

        private void FixedUpdate()
        {
            CheckBounds();
        }
        
        private void CheckBounds()
        {
            for (int n = bulletsService.Bullets.Count - 1; n >= 0; n--)
            {
                var thisBullet = bulletsService.Bullets[n];
                if (!levelBounds.InBounds(thisBullet.transform.position))
                    bulletPool.Release(thisBullet);
            }
        }
    }
}