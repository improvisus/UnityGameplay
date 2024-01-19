using System.Collections.Generic;
using Game.Level;
using UnityEngine;

namespace Game.Bullets
{
    public class BulletLevelBoundsController : MonoBehaviour
    {
        [SerializeField]
        private LevelBounds levelBounds;
        
        private BulletPool bulletPool;

        private readonly List<GameObject> bullets = new List<GameObject>();
        
        public void Construct(BulletPool bulletPool)
        {
            this.bulletPool = bulletPool;
        }
        
        private void OnEnable()
        {
            bulletPool.OnCreate += BulletFactoryOnCreate;
            bulletPool.OnRelease += BulletFactoryOnRelease;
        }

        private void OnDisable()
        {
            bulletPool.OnCreate -= BulletFactoryOnCreate;
            bulletPool.OnRelease -= BulletFactoryOnRelease;
        }
        
        private void BulletFactoryOnCreate(GameObject bullet)
        {
            bullets.Add(bullet);
        }
    
        private void BulletFactoryOnRelease(GameObject bullet)
        {
            bullets.Remove(bullet);
        }

        private void FixedUpdate()
        {
            CheckBounds();
        }
        
        private void CheckBounds()
        {
            for (int n = bullets.Count - 1; n >= 0; n--)
            {
                var thisBullet = bullets[n];
                if (!levelBounds.InBounds(thisBullet.transform.position))
                    bulletPool.Release(thisBullet);
            }
        }
    }
}