using Atomic.Elements;
using Atomic.Objects;
using Game.Actions;
using Game.Common;
using Game.Components;
using Game.Elements;
using Game.Mechanics;
using UnityEngine;

namespace Game.Objects.Projectiles
{
    public class Bullet : Projectile
    {
        [Section]
        public MoveComponent moveComponent;
        
        [Get(ObjectAPI.Transform)]
        [SerializeField]
        private Transform mainTransform;
        [Get(ObjectAPI.Damage)]
        [SerializeField]
        private AtomicVariable<int> damage;
        [SerializeField]
        private Countdown lifeTime = new(5);
        
        [Get(ObjectAPI.Reset)]
        private AtomicEvent resetEvent = new();
        private readonly AtomicEvent deathEvent = new();
        private readonly DealDamageAction dealDamageAction = new();
        
        private TimeEventMechanics timeEventMechanics;
        private TriggerMechanics triggerMechanics;
        
        private void Awake()
        {
            Compose();
        }
        
        public override void Compose()
        {
            base.Compose();
            
            moveComponent.Compose(mainTransform);
            dealDamageAction.Compose(damage);
            
            timeEventMechanics = new TimeEventMechanics(lifeTime, deathEvent);
            triggerMechanics = new TriggerMechanics(dealDamageAction, deathEvent);
            
            resetEvent.Subscribe(() => lifeTime.Reset() );
            deathEvent.Subscribe(() => projectilePool.Release(this));
        }
        
        protected override void Update()
        {
            base.Update();
            
            moveComponent.Update(Time.deltaTime);
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            
            timeEventMechanics.FixedUpdate(Time.fixedDeltaTime);
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
