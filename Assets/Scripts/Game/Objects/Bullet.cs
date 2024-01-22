using Atomic.Behaviours;
using Atomic.Elements;
using Atomic.Objects;
using Game.Actions;
using Game.Common;
using Game.Elements;
using Game.Mechanics;
using UnityEngine;

namespace Game.Objects
{
    public class Bullet : AtomicBehaviour
    {
        [SerializeField, Get(ObjectAPI.Transform)]
        private new Transform transform;
        [SerializeField]
        private AtomicVariable<float> speed = new AtomicVariable<float>(5000);
        [SerializeField, Get(ObjectAPI.MoveDirection)]
        private AtomicVariable<Vector3> direction = new();
        
        [SerializeField]
        private AtomicVariable<int> damage = new(1);
        private readonly Countdown countdown = new Countdown(5);
        
        private readonly AtomicEvent deathEvent = new();
        [Get(ObjectAPI.Reset)]
        private AtomicEvent resetEvent = new();

        private readonly DealDamageAction dealDamageAction = new DealDamageAction();
        
        private MovementMechanics movementMechanics;
        private LifetimeMechanics lifetimeMechanics;
        private TriggerMechanics triggerMechanics;
        private DestroyMechanics destroyMechanics;

        private void Awake()
        {
            Compose();
        }
        
        public override void Compose()
        {
            base.Compose();
            
            dealDamageAction.Compose(damage);
            
            movementMechanics = new MovementMechanics(transform, speed, direction);
            lifetimeMechanics = new LifetimeMechanics(countdown, deathEvent);
            triggerMechanics = new TriggerMechanics(dealDamageAction, deathEvent);
            destroyMechanics = new DestroyMechanics(deathEvent, this);
            
            resetEvent.Subscribe(() =>
            {
                countdown.Reset();
            });
        }
        
        protected override void OnEnable()
        {
            base.OnEnable();
            
            destroyMechanics.OnEnable();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            destroyMechanics.OnDisable();
        }
        
        protected override void Update()
        {
            base.Update();
            
            movementMechanics.Update(Time.deltaTime);
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
    }
}
