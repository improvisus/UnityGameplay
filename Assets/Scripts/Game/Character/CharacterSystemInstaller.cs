using Game.Bullets;
using Game.Common;
using Game.Components;
using Game.Input;
using UnityEngine;

namespace Game.Character
{
    public class CharacterSystemInstaller : Installer
    {
         [SerializeField]
         private CharacterService characterService;
         [SerializeField]
         private KeyboardHorizontalMoveInput keyboardHorizontalMoveInput;
         [SerializeField]
         private KeyboardFireInput keyboardFireInput;
         [SerializeField]
         private BulletPool bulletPool;
         
         [SerializeField]
         private CharacterMoveController characterMoveController;
         [SerializeField]
         private CharacterFireController characterFireController;
         [SerializeField]
         private CharacterDeathObserver characterDeathObserver;
        
        protected override void Install()
        {
            characterMoveController.Construct(characterService, keyboardHorizontalMoveInput);
            characterFireController.Construct(characterService, keyboardFireInput);
            characterDeathObserver.Construct(characterService);

            var weaponComponent = characterService.Character.GetComponent<WeaponComponent>();
            weaponComponent.Construct(bulletPool);
        }
    }
}