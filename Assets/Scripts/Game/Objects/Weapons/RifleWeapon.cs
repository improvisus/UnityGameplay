using System;
using Atomic.Elements;
using Atomic.Objects;
using Game.Actions;
using Game.Elements;
using UnityEngine;

namespace Game.Objects
{
    public class RifleWeapon : Weapon
    {
        public override IAtomicValue<bool> CanAttack { get; } = new AtomicValue<bool>(true);
        public override IAtomicAction AttackAction => attackAction;

        [Section]
        [SerializeField]
        private View view;
        
        [SerializeField]
        private Countdown fireCountdown = new(2.0f);
        [SerializeField]
        private AtomicVariable<int> damage = new(1);
        [SerializeField]
        private AtomicVariable<LayerMask> layerMask;
        [SerializeField]
        private Transform attackPoint;
        
        private readonly AtomicEvent fireEvent = new();
        private readonly AtomicAction attackAction = new();
        private readonly DealDamageAction dealDamageAction = new();
        private readonly RayCastAction rayCastAction = new();

        private void Awake()
        {
            Compose();
        }
        
        public override void Compose()
        {
            base.Compose();
            
            view.Compose(this);
            
            dealDamageAction.Compose(damage);
            
            rayCastAction.Compose(dealDamageAction, attackPoint, layerMask);
            
            attackAction.Compose(() =>
            {
                if (CanAttack.Value)
                {
                    fireEvent?.Invoke();
                    rayCastAction?.Invoke();
                }
            });
        }
        
        private void FixedUpdate()
        {
            fireCountdown.Update(Time.fixedDeltaTime);
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(attackPoint.position, attackPoint.forward * 100);
        }
        
        private void OnDestroy()
        {
            fireEvent?.Dispose();
            damage?.Dispose();
            layerMask?.Dispose();
        }
        
        [Serializable]
        private class View
        {
            [SerializeField]
            private ParticleSystem fireVFX;
            
            public void Compose(RifleWeapon weapon)
            {
                weapon.fireEvent.Subscribe(() => fireVFX.Play(withChildren: true));
            }
        }
    }
}