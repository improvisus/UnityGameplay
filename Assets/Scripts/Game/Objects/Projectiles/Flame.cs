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
    public class Flame : Projectile
    {
        [Section]
        public MoveComponent moveComponent;
     
        [Get(ObjectAPI.Transform)]
        [SerializeField]
        private Transform mainTransform;
        [SerializeField]
        private SphereCollider sphereCollider;
        [SerializeField]
        private AtomicVariable<int> damage = new(1);
        [SerializeField]
        private AtomicVariable<float> lengthFire = new(5);
        [SerializeField]
        private AtomicVariable<float> radiusFire = new(0.5f);
        
        private Countdown lifeTime;
        
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
            
            lifeTime = new(lengthFire.Value / moveComponent.MoveSpeed.Value);
            
            radiusFire.Subscribe((radius) =>
            {
                sphereCollider.radius = radius;
                var centerSphere = sphereCollider.center;
                centerSphere.x = radius;
                sphereCollider.center = centerSphere;
                
            });
            
            lengthFire.Subscribe((length) => lifeTime.duration = length / moveComponent.MoveSpeed.Value);
            
            moveComponent.Compose(mainTransform);
            dealDamageAction.Compose(damage);
            
            timeEventMechanics = new TimeEventMechanics(lifeTime, deathEvent);
            triggerMechanics = new TriggerMechanics(dealDamageAction, null);
            
            resetEvent.Subscribe(() => lifeTime.Reset() );
            deathEvent.Subscribe(() => projectilePool.Release(this));
        }

        public void Start()
        {
            sphereCollider.radius = radiusFire.Value;
            var centerSphere = sphereCollider.center;
            centerSphere.x = radiusFire.Value;
            sphereCollider.center = centerSphere;
        }
        
        private void Update()
        {
            moveComponent.Update(Time.deltaTime);
        }

        private void FixedUpdate()
        {
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
            lengthFire?.Dispose();
            radiusFire?.Dispose();
            resetEvent?.Dispose();
            deathEvent?.Dispose();
        }
    }
}