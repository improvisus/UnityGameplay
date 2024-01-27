using System.Collections.Generic;
using UnityEngine;

namespace Game.Bullets
{
    public class BulletsService : MonoBehaviour
    {
        public List<GameObject> Bullets => bullets;
        
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
    }
}