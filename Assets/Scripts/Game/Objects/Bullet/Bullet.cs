using Atomic.Behaviours;
using Atomic.Elements;
using Atomic.Objects;
using Game.Actions;
using Game.Common;
using Game.Components;
using Game.Controllers;
using Game.Elements;
using Game.Mechanics;
using UnityEngine;

namespace Game.Objects
{
    public class Bullet : AtomicBehaviour
    {
        [SerializeField, Get(ObjectAPI.Transform)]
        private Transform mainTransform;
        
        [SerializeField, Section]
        private MoveComponent moveComponent;
        
        [SerializeField]
        private AtomicVariable<int> damage = new(1);
        private readonly Countdown countdown = new(5);
        
        private readonly AtomicEvent deathEvent = new();
        
        [Get(ObjectAPI.Reset)]
        private AtomicEvent resetEvent = new();

        private readonly DealDamageAction dealDamageAction = new();
        
        private LifetimeMechanics lifetimeMechanics;
        private TriggerMechanics triggerMechanics;
        private DestroyMechanics destroyMechanics;

        private BulletPool bulletPool;
        
        public void Construct(BulletPool bulletPool)
        {
            this.bulletPool = bulletPool;
        }
        
        private void Awake()
        {
            Compose();
        }
        
        public override void Compose()
        {
            base.Compose();
            
            moveComponent.Compose(mainTransform);
            dealDamageAction.Compose(damage);
            
            lifetimeMechanics = new LifetimeMechanics(countdown, deathEvent);
            triggerMechanics = new TriggerMechanics(dealDamageAction, deathEvent);
            
            resetEvent.Subscribe(() => countdown.Reset() );
            deathEvent.Subscribe(() => bulletPool.Release(this));
        }
        
        protected override void Update()
        {
            base.Update();
            moveComponent.Update(Time.deltaTime);
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            lifetimeMechanics.FixedUpdate(Time.fixedDeltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<IAtomicObject>(out var target))
                triggerMechanics.OnTriggerEnter(target);
        }
        
        private void OnDestroy()
        {
            damage?.Dispose();
            deathEvent?.Dispose();
            resetEvent?.Dispose();
        }
    }
}
