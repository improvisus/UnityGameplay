using Atomic.Elements;
using Game.Actions;
using Game.Controllers;
using Game.Elements;
using Game.Engine;
using Game.Mechanics;
using UnityEngine;

namespace Game.Objects
{
    public class FlamethrowerWeapon : Weapon
    {
        public override IAtomicValue<bool> CanAttack => fireCondition;
        public override IAtomicAction AttackAction => attackAction;

        [SerializeField]
        private ProjectilePool projectilePool;
        [SerializeField]
        private AtomicVariable<int> damage = new(1);
        [SerializeField]
        private Transform firePoint;
        [SerializeField]
        private Countdown timeAttack = new(0.6f);
        [SerializeField]
        private Countdown timePeriod = new(0.03f);
        
        private readonly ProjectileCreateAction projectileCreateAction = new();
        private readonly AtomicAction attackAction = new();
        private readonly AtomicFunction<bool> fireCondition = new();
        private readonly AtomicEvent endAttackEvent = new();
        
        private TimeEventMechanics timeEventMechanics;
        private PeriodMechanics periodMechanics;

        private void Awake()
        {
            Compose();
        }
        
        public override void Compose()
        {
            base.Compose();
            
            fireCondition.Compose(() => true);

            projectileCreateAction.Compose(projectilePool, damage, firePoint);
            
            attackAction.Compose(() =>
            {
                if (fireCondition.Value)
                {
                    timeAttack.Reset();
                    timePeriod.Reset();
                }
            });
            
            endAttackEvent.Subscribe(() =>
            {
                timeAttack.Stop();
                timePeriod.Stop();
            });
            
            timeEventMechanics = new TimeEventMechanics(timeAttack, endAttackEvent);
            periodMechanics = new PeriodMechanics(timePeriod, projectileCreateAction);
        }

        private void Start()
        {
            timeAttack.Stop();
            timePeriod.Stop();
        }
        
        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            
            timeEventMechanics.FixedUpdate(Time.fixedDeltaTime);
            periodMechanics.FixedUpdate(Time.fixedDeltaTime);
        }
        
        private void OnDestroy()
        {
            endAttackEvent?.Dispose();
        }
    }
}