using System;
using Atomic.Elements;
using Atomic.Objects;
using Game.Actions;
using UnityEngine;

namespace Game.Objects
{
    public class KnifeWeapon : Weapon
    {
        public override IAtomicValue<bool> CanAttack { get; } = new AtomicValue<bool>(true);
        public override IAtomicAction AttackAction => attackAction;

        [Section]
        [SerializeField]
        private View view;
        
        [SerializeField]
        private AtomicVariable<int> damage = new(1);
        [SerializeField]
        private AtomicVariable<float> radiusAttack;
        [SerializeField]
        private AtomicVariable<float> distanceAttack;
        [SerializeField]
        private AtomicVariable<LayerMask> layerMask;
        [SerializeField]
        private Transform attackPoint;
        
        private readonly AtomicEvent fireEvent = new();
        private readonly AtomicAction attackAction = new();
        private readonly DealDamageAction dealDamageAction = new();
        private readonly ShapeCastAction shapeCastAction = new();

        private void Awake()
        {
            Compose();
        }
        
        public override void Compose()
        {
            base.Compose();
            
            view.Compose(this);
            
            dealDamageAction.Compose(damage);

            shapeCastAction.Compose(dealDamageAction, attackPoint, radiusAttack, distanceAttack, layerMask);

            attackAction.Compose(() =>
            {
                if (CanAttack.Value)
                {
                    fireEvent?.Invoke();
                    shapeCastAction?.Invoke();
                }
            });
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position + transform.forward * distanceAttack.Value, radiusAttack.Value);
        }
        
        private void OnDestroy()
        {
            fireEvent?.Dispose();
            damage?.Dispose();
            radiusAttack?.Dispose();
            distanceAttack?.Dispose();
            layerMask?.Dispose();
        }
        
        [Serializable]
        private class View
        {
            [SerializeField]
            private ParticleSystem fireVFX;
            
            public void Compose(KnifeWeapon weapon)
            {
                weapon.fireEvent.Subscribe(() => fireVFX.Play(withChildren: true));
            }
        }
    }
}