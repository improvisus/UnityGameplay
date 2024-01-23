using Game.Common;
using Game.Controllers.Bullet;
using Game.Input;
using UnityEngine;

namespace Game.Controllers.Character
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
            characterFireController.Constructs(characterService, bulletPool, mouseFireInput);
        }
    }
}