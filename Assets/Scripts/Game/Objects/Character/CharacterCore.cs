using System;
using Atomic.Objects;
using Game.Common;
using Game.Components;
using Game.Mechanics;
using UnityEngine;

namespace Game.Objects
{
    [Serializable]
    public class CharacterCore : IDisposable
    {
        [SerializeField, Get(ObjectAPI.Transform)]
        private Transform mainTransform;
        
        [Section]
        public MoveComponent moveComponent;
        [Section]
        public HealthComponent healthComponent;
        [Section]
        public FireComponent fireComponent;
        
        private RotationMechanics rotationMechanics;
        private DestroyMechanics destroyMechanics;
        
        public void Compose(AtomicObject atomicObject)
        {
            moveComponent.Compose(mainTransform);
            healthComponent.Compose();
            fireComponent.Compose();

            fireComponent.FireCondition.Append(moveComponent.IsNotMoving);
            
            destroyMechanics = new DestroyMechanics(healthComponent.DeathEvent, atomicObject);
            rotationMechanics = new RotationMechanics(mainTransform, moveComponent.Direction);
        }

        public void OnEnable()
        {
            healthComponent.OnEnable();
            destroyMechanics.OnEnable();
        }

        public void OnDisable()
        {
            healthComponent.OnDisable();
            destroyMechanics.OnDisable();
        }
        
        public void Update(float deltaTime)
        {
            moveComponent.Update(deltaTime);
            rotationMechanics.Update(deltaTime);
        }
        
        public void Dispose()
        {
            healthComponent?.Dispose();
        }
    }
}