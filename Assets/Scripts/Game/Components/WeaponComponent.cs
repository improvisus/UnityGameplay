using System;
using System.Collections.Generic;
using Atomic.Elements;
using Atomic.Objects;
using Game.Common;
using Game.Engine;
using Game.Objects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Components
{
    [Serializable]
    public class WeaponComponent
    {
        public IAtomicVariable<Weapon> CurrentWeapon => currentWeapon;
        
        [SerializeField]
        private List<Weapon> weapons;
        [SerializeField]
        private AtomicVariable<Weapon> currentWeapon;
        
        [Get(ObjectAPI.SwitchWeaponAction)]
        private AtomicAction<int> switchWeaponAction = new();
        
        public void Compose()
        {
            switchWeaponAction.Compose((id) =>
            {
                if (weapons.Count > id && weapons[id] != null)
                {
                    currentWeapon.Value.gameObject.SetActive(false);
                    currentWeapon.Value = weapons[id];
                    currentWeapon.Value.gameObject.SetActive(true);
                }
            });
        }
    }
}