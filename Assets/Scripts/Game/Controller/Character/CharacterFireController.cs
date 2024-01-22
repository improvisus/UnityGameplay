using Game.Common;
using Game.Controller.Bullet;
using Game.Input;
using UnityEngine;

namespace Game.Controller.Character
{
    public class CharacterFireController : MonoBehaviour
    {
        private CharacterService characterService;
        
        private BulletPool bulletPool;
        
        private IFireInput fireInput;
        
        public void Construct(CharacterService characterService, BulletPool bulletPool, IFireInput fireInput)
        {
            this.characterService = characterService;
            this.bulletPool = bulletPool;
            this.fireInput = fireInput;
        }

        public void OnEnable()
        {
            fireInput.OnFire += OnFire;
        }
        
        public void OnDisable()
        {
            fireInput.OnFire -= OnFire;
        }
        
        private void OnFire()
        {
            var firePoint = characterService.Character.Get<Transform>(ObjectAPI.FirePoint);
            bulletPool.Create(new BulletPool.Args
            {
                FirePoint = firePoint
            });
        }
    }
}
