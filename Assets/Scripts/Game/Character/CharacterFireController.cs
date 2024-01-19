using Game.Bullets;
using Game.Components;
using Game.Input;
using UnityEngine;

namespace Game.Character
{
    public class CharacterFireController : MonoBehaviour
    {
        private CharacterService characterService;
        
        private BulletPool bulletFactory;
        
        private IFireInput fireInput;
        
        private WeaponComponent weaponComponent;
        
        public void Construct(CharacterService characterService, BulletPool bulletFactory, IFireInput fireInput)
        {
            this.characterService = characterService;
            this.bulletFactory = bulletFactory;
            this.fireInput = fireInput;
        }
        
        private void Awake()
        {
            weaponComponent = characterService.Character.GetComponent<WeaponComponent>();
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
            var bulletConfig = weaponComponent.BulletConfig;
            bulletFactory.Create(new BulletPool.Args
            {
                teamType = bulletConfig.teamType,
                physicsLayer = bulletConfig.physicsLayer,
                color = bulletConfig.color,
                damage = bulletConfig.damage,
                position = weaponComponent.Position,
                direction = Vector2.up,
                speed = bulletConfig.speed
            });
        }
    }
}