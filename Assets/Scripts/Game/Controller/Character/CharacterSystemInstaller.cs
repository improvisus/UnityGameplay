using Game.Common;
using Game.Controller.Bullet;
using Game.Input;
using UnityEngine;

namespace Game.Controller.Character
{
    public class CharacterSystemInstaller : Installer
    {
        [SerializeField]
        private CharacterService characterService;
        [SerializeField]
        private MouseFireInput mouseFireInput;
        [SerializeField]
        private BulletPool bulletPool;
        
        [SerializeField]
        private CharacterFireController characterFireController;
        
        protected override void Install()
        {
            characterFireController.Construct(characterService, bulletPool, mouseFireInput);
        }
    }
}