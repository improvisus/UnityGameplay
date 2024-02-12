using System;
using Atomic.Elements;
using Atomic.Objects;
using Game.Common;
using Game.Expressions;
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
        public IAtomicValue<float> MoveSpeed => fullSpeed;
        
        [Get(ObjectAPI.MoveSpeedExpression)]
        public IAtomicExpression<float> MoveSpeedExpression => fullSpeed;
        [SerializeField]
        private FloatProductExpression fullSpeed;
        
        [SerializeField]
        private AtomicVariable<float> baseSpeed = new(5);
        [Get(ObjectAPI.MoveDirection)]
        [SerializeField]
        private AtomicVariable<Vector3> direction = new();
        
        [SerializeField]
        private AtomicFunction<bool> isMoving = new();
        [SerializeField]
        private AtomicFunction<bool> isNotMoving = new();
        
        private MovementMechanics movementMechanics;
        
        public void Compose(Transform transform)
        {
            isMoving.Compose(() => fullSpeed.Invoke() != 0 && direction.Value.magnitude > 0);
            isNotMoving.Compose(() => direction.Value.magnitude == 0);
            
            fullSpeed.Append(baseSpeed);
            
            movementMechanics = new MovementMechanics(transform, fullSpeed, direction);
        }
        
        public void Update(float deltaTime)
        {
            movementMechanics.Update(deltaTime);
        }
    }
}