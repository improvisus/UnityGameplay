using System;
using Atomic.Elements;
using Atomic.Objects;
using Game.Common;
using Game.Objects;
using UnityEngine;

namespace Game.Components
{
    [Serializable]
    public class AttackComponent
    {
        public AtomicFunction<bool> AttackCondition => attackCondition;
        public IAtomicEvent AttackRequest => attackRequest;
        public IAtomicAction AttackAction => attackAction;
        
        [Get(ObjectAPI.AttackRequest)]
        [SerializeField]
        private AtomicEvent attackRequest = new();
        private AtomicAction attackAction = new();
        private AtomicFunction<bool> attackCondition = new();
        
        public void Compose(WeaponComponent weaponComponent)
        {
            attackAction.Compose(() =>
            {
                if(attackCondition.Invoke())
                    weaponComponent.CurrentWeapon.Value.AttackAction?.Invoke();
            });
        }
    }
}