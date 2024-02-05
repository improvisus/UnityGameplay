using System;
using Atomic.Elements;
using Atomic.Objects;
using Game.Actions;
using Game.Controllers;
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
            
            core.Compose();
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
            private Transform firePoint;
        
            private readonly AtomicAction attackAction = new();
            private readonly ProjectileCreateAction projectileCreateAction = new();
            private readonly AtomicEvent fireEvent = new();
            private readonly AtomicFunction<bool> fireCondition = new();

            public void Compose()
            {
                fireCondition.Compose(() => !magazine.IsEmpty);
                
                projectileCreateAction.Compose(projectilePool, firePoint);
                
                attackAction.Compose(() =>
                {
                    if (fireCondition.Value)
                    {
                        fireEvent?.Invoke();
                        projectileCreateAction?.Invoke();
                    }
                });
                
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