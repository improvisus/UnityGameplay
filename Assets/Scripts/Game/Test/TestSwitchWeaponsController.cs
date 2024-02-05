using Atomic.Elements;
using Game.Common;
using Game.Controllers;
using UnityEngine;

namespace Game.Test
{
    public class TestSwitchWeaponsController : MonoBehaviour
    {
        private HeroService heroService;
        
        public void Constructs(HeroService heroService)
        {
            this.heroService = heroService;
        }
        
        private void Update()
        {
            if (UnityEngine.Input.GetKey(KeyCode.Alpha1))
                SwitchWeapon(0);
            if (UnityEngine.Input.GetKey(KeyCode.Alpha2))
                SwitchWeapon(1);
            if (UnityEngine.Input.GetKey(KeyCode.Alpha3))
                SwitchWeapon(2);
            if (UnityEngine.Input.GetKey(KeyCode.Alpha4))
                SwitchWeapon(3);
            if (UnityEngine.Input.GetKey(KeyCode.Alpha5))
                SwitchWeapon(4);
        }
        
        private void SwitchWeapon(int id)
        {
            var switchWeapon = heroService.Character.Get<IAtomicAction<int>>(ObjectAPI.SwitchWeaponAction);
            switchWeapon?.Invoke(id);
        }
    }
}