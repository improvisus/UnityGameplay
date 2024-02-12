using System;
using Atomic.Elements;
using Atomic.Objects;
using Game.Actions;
using Game.Common;
using Game.Controllers;
using Game.Effects;
using Game.Expressions;
using UnityEngine;

namespace Game.Objects
{
    public class MachineGunWeapon : Weapon
    {
        public override IAtomicValue<bool> CanAttack => core.FireCondition;
        public override IAtomicAction AttackAction => core.BulletCreateAction;

        [Section]
        [SerializeField]
        private Core core;
        
        [Section]
        [SerializeField]
        private View view;
        
        private void Awake()
        {
            Compose();
        }
        
        public override void Compose()
        {
            base.Compose();
            
            core.Compose(this);
            view.Compose(core);
        }
        
        private void OnDestroy()
        {
            core?.Dispose();
        }
        
        [Serializable]
        private class Core: IDisposable
        {
            public IAtomicAction BulletCreateAction => attackAction;
            public IAtomicFunction<bool> FireCondition => fireCondition;
            public IAtomicEvent FireEvent => fireEvent;
            
            [SerializeField]
            private WeaponMagazine magazine;
            [SerializeField]
            private ProjectilePool projectilePool;
            [SerializeField]
            private AtomicVariable<int> baseDamage = new(1);
            [SerializeField]
            private Transform firePoint;
            
            [Get(ObjectAPI.DamageExpression)]
            public IAtomicExpression<int> Damage => fullDamage;
            [SerializeField]
            private IntSumExpression fullDamage;
            
            private readonly AtomicAction attackAction = new();
            private readonly ProjectileCreateAction projectileCreateAction = new();
            private readonly AtomicEvent fireEvent = new();
            private readonly AtomicFunction<bool> fireCondition = new();

            [Get(ObjectAPI.EffectManager)]
            [SerializeField]
            private EffectManager effectManager;
            
            public void Compose(Weapon weapon)
            {
                fireCondition.Compose(() => !magazine.IsEmpty);
                
                fullDamage.Append(baseDamage);
                
                projectileCreateAction.Compose(projectilePool, fullDamage, firePoint);
                
                attackAction.Compose(() =>
                {
                    if (fireCondition.Value)
                    {
                        fireEvent?.Invoke();
                        projectileCreateAction?.Invoke();
                    }
                });
                
                effectManager.Compose(weapon);
                
                fireEvent.Subscribe(() => magazine.SpendCharge());
            }
            
            public void Dispose()
            {
                fireEvent?.Dispose();
            }
        }
        [Serializable]
        private class View
        {
            [SerializeField]
            private AudioSource audioSource;
            [SerializeField]
            private AudioClip fireSFX;
            [SerializeField]
            private ParticleSystem fireVFX;
            
            public void Compose(Core core)
            {
                core.FireEvent.Subscribe(() => fireVFX.Play(withChildren: true));
                core.FireEvent.Subscribe(() => audioSource.PlayOneShot(fireSFX));
            }
        }
    }
}