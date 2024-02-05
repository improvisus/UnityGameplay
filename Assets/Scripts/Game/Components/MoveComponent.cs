using System;
using Atomic.Elements;
using Atomic.Objects;
using Game.Common;
using Game.Mechanics;
using UnityEngine;

namespace Game.Components
{
    [Serializable]
    public class MoveComponent
    {
        public IAtomicValue<bool> IsMoving => isMoving;
        public IAtomicValue<bool> IsNotMoving => isNotMoving;
        public IAtomicVariable<Vector3> Direction => direction;
        public IAtomicValue<float> Speed => speed;

        [SerializeField]
        private AtomicVariable<float> speed = new(5);
        [SerializeField, Get(ObjectAPI.MoveDirection)]
        private AtomicVariable<Vector3> direction = new();
        
        [SerializeField]
        private AtomicFunction<bool> isMoving = new();
        [SerializeField]
        private AtomicFunction<bool> isNotMoving = new();
        
        private MovementMechanics movementMechanics;
        
        public void Compose(Transform transform)
        {
            isMoving.Compose(() => direction.Value.magnitude > 0);
            isNotMoving.Compose(() => direction.Value.magnitude == 0);
            
            movementMechanics = new MovementMechanics(transform, speed, direction);
        }
        
        public void Update(float deltaTime)
        {
            movementMechanics.Update(deltaTime);
        }
    }
}