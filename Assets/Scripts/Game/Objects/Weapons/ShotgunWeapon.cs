using System;
using Atomic.Elements;
using Atomic.Objects;
using Game.Actions;
using UnityEditor;
using UnityEngine;

namespace Game.Objects
{
    public class ShotgunWeapon : Weapon
    {
        public override IAtomicValue<bool> CanAttack => fireCondition;
        public override IAtomicAction AttackAction => attackAction;
        
        [Section]
        [SerializeField]
        private View view;
        
        [SerializeField]
        private WeaponMagazine magazine;
        [SerializeField]
        private AtomicVariable<int> damage = new(1);
        [SerializeField]
        private AtomicVariable<LayerMask> layerMask;
        [SerializeField]
        private Transform attackPoint;
        [SerializeField]
        private AtomicVariable<float> lengthAttack;
        [SerializeField]
        private AtomicVariable<float> angleAttack;
        
        private readonly AtomicAction attackAction = new();
        private readonly DealDamageAction dealDamageAction = new();
        private readonly ConeCastAction coneCastAction = new();
        private readonly AtomicEvent fireEvent = new();
        private readonly AtomicFunction<bool> fireCondition = new();
        
        private void Awake()
        {
            Compose();
        }
        
        public override void Compose()
        {
            base.Compose();
            
            view.Compose(this);
            
            dealDamageAction.Compose(damage);
            
            fireCondition.Compose(() => !magazine.IsEmpty);
            
            coneCastAction.Compose(dealDamageAction, attackPoint, lengthAttack, angleAttack, layerMask);
            
            attackAction.Compose(() =>
            {
                if (CanAttack.Value)
                {
                    fireEvent?.Invoke();
                    coneCastAction?.Invoke();
                }
            });
            
            fireEvent.Subscribe(() => magazine.SpendCharge());
        }
        
        private void OnDrawGizmos()
        {
            DrawCone(attackPoint.position, attackPoint.rotation, lengthAttack.Value, angleAttack.Value, Color.red);
        }
        
        private static void DrawCone(Vector3 position, Quaternion rotation, float length, float angle, Color color, int countLine = 4)
        {
            if(!Application.isPlaying)
                return;
            
            var endAngle = Mathf.Tan(angle * 0.5f * Mathf.Deg2Rad) * length;
            var forward = rotation * Vector3.forward;
            var endPosition = position + forward * length;
            var radius = length * Mathf.Tan(angle / 2 * Mathf.Deg2Rad);

            Handles.color = color;
            
            Handles.DrawWireDisc(endPosition, forward, radius);

            for (var n = 0; n < countLine; n++)
            {
                float a = n * (360f / countLine) * Mathf.Deg2Rad;
                Vector3 point = rotation * new Vector3(Mathf.Cos(a), Mathf.Sin(a)) * endAngle;
                Handles.DrawLine(position, position + point + forward * length);
            }
        }

        private void OnDestroy()
        {
            fireEvent?.Dispose();
            damage?.Dispose();
            layerMask?.Dispose();
            lengthAttack?.Dispose();
            angleAttack?.Dispose();
        }
        
        [Serializable]
        private class View
        {
            [SerializeField]
            private ParticleSystem fireVFX;
            
            public void Compose(ShotgunWeapon weapon)
            {
                weapon.fireEvent.Subscribe(() => fireVFX.Play(withChildren: true));
            }
        }
    }
}