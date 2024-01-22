using Engine.Common;
using Game.Input;
using UnityEngine;

namespace Game.Controller
{
    public class CharacterSystemInstaller : Installer
    {
        [SerializeField]
        private CharacterService characterService;
        [SerializeField]
        private KeyboardFireInput keyboardFireInput;

        [SerializeField]
        private CharacterFireController characterFireController;
        
        protected override void Install()
        {
            characterFireController.Construct(characterService, keyboardFireInput);
        }
    }
}