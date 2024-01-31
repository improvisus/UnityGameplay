using System;
using Atomic.Elements;
using Atomic.Objects;
using Game.Actions;
using Game.Common;
using Game.Elements;
using Game.Mechanics;
using UnityEngine;

namespace Game.Components
{
    [Serializable]
    [Is(ObjectType.Damageable)]
    public class HealthComponent : IDisposable
    {
        public AtomicEvent DeathEvent => deathEvent;
        
        [SerializeField]
        private HitPoints hitPoints = new(5, 5);
        
        private readonly AtomicEvent deathEvent = new();
        
        [SerializeField, Get(ObjectAPI.TakeDamageAction)]
        private TakeDamageAction takeDamageAction = new();
        
        private DeathMechanics deathMechanics;
        
        public void Compose()
        {
            takeDamageAction.Compose(hitPoints);
            deathMechanics = new DeathMechanics(hitPoints, deathEvent);
        }
        
        public void OnEnable()
        {
            deathMechanics.OnEnable();
        }

        public void OnDisable()
        {
            deathMechanics.OnDisable();
        }
        
        public void Dispose()
        {
            deathEvent?.Dispose();
        }
    }
}