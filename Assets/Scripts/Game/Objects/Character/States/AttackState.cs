using AIModule;
using Atomic.Extensions;
using Atomic.Objects;
using Game.Common;
using UnityEngine;

namespace Game.Objects.States
{
    public class AttackState : IAIState
    {
        private IAtomicObject target;

        public AttackState(IAtomicObject target)
        {
            this.target = target;
        }

        public void OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if (!blackboard.TryGetObject(BlackboardAPI.Character, out IAtomicObject character) ||
                !blackboard.TryGetFloat(BlackboardAPI.AttackDistance, out float attackDistance))
            {
                return;
            }
            
            if (!character.TryGet(ObjectAPI.Transform, out Transform transform))
                return;


            // if (target == null || target.Equals(null))
            // {
            //     target = blackboard.GetObject<IAtomicObject>(BlackboardAPI.Target);

           // }
            
           if (target == null || target.Equals(null))
               return;
           
            if (!target.TryGet(ObjectAPI.Transform, out Transform transformTarget))
                return;
            
            var moveDirection = character.GetVariable<Vector3>(ObjectAPI.MoveDirection);
            var currentPosition = transform.position;
            var distanceVector = transformTarget.position - currentPosition;
            
            if (distanceVector.sqrMagnitude <= attackDistance * attackDistance)
            {
                moveDirection.Value = Vector3.zero;
                character.InvokeAction(ObjectAPI.AttackRequest);
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