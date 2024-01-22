using Atomic.Behaviours;
using Atomic.Elements;
using Atomic.Objects;
using Game.Common;
using Game.Elements;
using Game.Mechanics;
using UnityEngine;

namespace Game.Objects
{
    [Is(ObjectAPI.Damageable)]
    public class Character : AtomicBehaviour
    {
        [SerializeField]
        private HitPoints hitPoints = new HitPoints(5, 5);
        [Get(ObjectAPI.TakeDamageAction)]
        private AtomicEvent<int> takeDamageEvent = new();
        private readonly AtomicEvent deathEvent = new();
        
        [SerializeField, Get(ObjectAPI.FirePoint)]
        private Transform firePoint;
        [SerializeField, Get(ObjectAPI.FireEvent)]
        private AtomicEvent fireEvent;
        [SerializeField]
        public AtomicObject bulletPrefab;
        
        private TakeDamageMechanics takeDamageMechanics;
        private DeathMechanics deathMechanics;
        private FireBulletMechanics fireBulletMechanics; // ставил без пулла для управление вручную для теста чтобы проверить стрельбу врага
        private DestroyMechanics destroyMechanics;

        private void Awake()
        {
            Compose();
        }
        
        public override void Compose()
        {
            base.Compose();
            
            takeDamageMechanics = new TakeDamageMechanics(hitPoints, takeDamageEvent);
            deathMechanics = new DeathMechanics(hitPoints, deathEvent);
            fireBulletMechanics = new FireBulletMechanics(bulletPrefab, firePoint, fireEvent);
            destroyMechanics = new DestroyMechanics(deathEvent, this);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            
            takeDamageMechanics.OnEnable();
            deathMechanics.OnEnable();
            fireBulletMechanics.OnEnable();
            destroyMechanics.OnEnable();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            takeDamageMechanics.OnDisable();
            deathMechanics.OnDisable();
            fireBulletMechanics.OnDisable();
            destroyMechanics.OnDisable();
        }
    }
}
