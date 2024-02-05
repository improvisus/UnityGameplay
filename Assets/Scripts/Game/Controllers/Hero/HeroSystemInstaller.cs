using Game.Common;
using Game.Input;
using Game.Test;
using UnityEngine;
using UnityEngine.Serialization;

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
        [FormerlySerializedAs("testSwitchWeapons")]
        [SerializeField]
        private TestSwitchWeaponsController testSwitchWeaponsController;
        
        protected override void Install()
        {
            heroMoveController.Constructs(heroService, keyboardMoveInput);
            heroFireController.Constructs(heroService, mouseFireInput);
            testSwitchWeaponsController.Constructs(heroService);
        }
    }
}