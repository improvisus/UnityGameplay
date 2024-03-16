using AIModule;
using Atomic.Extensions;
using Atomic.Objects;
using Game.Common;
using UnityEngine;

namespace Game.Objects.States
{
    public class MoveToTargetState : IAIState
    {
        private IAtomicObject target;

        public MoveToTargetState(IAtomicObject target)
        {
            this.target = target;
        }

        public void OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if (!blackboard.TryGetObject(BlackboardAPI.Character, out IAtomicObject character) ||
                !blackboard.TryGetFloat(BlackboardAPI.StoppingDistance, out float stoppingDistance))
            {
                return;
            }
            
            if (!character.TryGet(ObjectAPI.Transform, out Transform transform))
                return;
            
            if (!target.TryGet(ObjectAPI.Transform, out Transform transformTarget))
                return;
            
            var moveDirection = character.GetVariable<Vector3>(ObjectAPI.MoveDirection);
            var currentPosition = transform.position;
            var distanceVector = transformTarget.position - currentPosition;
            
            if (distanceVector.sqrMagnitude <= stoppingDistance * stoppingDistance)
            {
                moveDirection.Value = Vector3.zero;
                return;
            }
            
            moveDirection.Value = Vector3.ClampMagnitude(distanceVector, 1);
        }
        
        public void OnStart(IBlackboard blackboard)
        {
            blackboard.SetBool(BlackboardAPI.TargetSensorEnabled, false);
        }
        public void OnStop(IBlackboard blackboard)
        {
            blackboard.SetBool(BlackboardAPI.TargetSensorEnabled, true);
            
            if (!blackboard.TryGetObject(BlackboardAPI.Character, out IAtomicObject character))
                return;
            
            var moveDirection = character.GetVariable<Vector3>(ObjectAPI.MoveDirection);
            moveDirection.Value = Vector3.zero;
        }
    }
}