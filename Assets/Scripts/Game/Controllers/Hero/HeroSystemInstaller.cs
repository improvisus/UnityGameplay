using Game.Common;
using Game.Input;
using UnityEngine;

namespace Game.Controllers
{
    public class HeroSystemInstaller : Installer
    {
        [SerializeField]
        private HeroService heroService;
        [SerializeField]
        private MouseFireInput mouseFireInput;
        [SerializeField]
        private KeyboardMoveInput keyboardMoveInput;
        
        [SerializeField]
        private HeroMoveController heroMoveController;
        [SerializeField]
        private HeroFireController heroFireController;
        
        protected override void Install()
        {
            heroMoveController.Constructs(heroService, keyboardMoveInput);
            heroFireController.Constructs(heroService, mouseFireInput);
        }
    }
}