using AIModule;
using Atomic.Extensions;
using Atomic.Objects;
using UnityEngine;

namespace Game.Engine.AI.BTNodes
{

    public class BTNode_MoveToTarget : BTNode
    {
        public override string Name => "Move To Target";
        
        [SerializeField, BlackboardKey] private ushort character;
        [SerializeField, BlackboardKey] private ushort target;
        [SerializeField, BlackboardKey] private ushort stoppingDistance;

        protected override BTState OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if (!blackboard.TryGetObject(this.character, out IAtomicObject character) ||
                !blackboard.TryGetObject(this.target, out IAtomicObject target) ||
                !blackboard.TryGetFloat(this.stoppingDistance, out float stoppingDistance))
            {
                return BTState.FAILURE;
            }

            if (!character.TryGet(ObjectAPI.Transform, out Transform characterTransform) ||
                !target.TryGet(ObjectAPI.Transform, out Transform targetTransform))
            {
                return BTState.FAILURE;
            }
    
            Vector3 currentPosition = characterTransform.position;
            Vector3 targetPosition = targetTransform.position;
            Vector3 distanceVector = targetPosition - currentPosition;

            if (distanceVector.sqrMagnitude <= stoppingDistance * stoppingDistance)
            {
                character.InvokeAction(ObjectAPI.MoveRequest, Vector3.zero);
                return BTState.SUCCESS;
            }
            
            distanceVector = distanceVector.normalized;
            character.InvokeAction(ObjectAPI.MoveRequest, distanceVector);
            return BTState.RUNNING;
        }

        protected override void OnEnable(IBlackboard blackboard)
        {
        }

        protected override void OnDisable(IBlackboard blackboard)
        {
            if (blackboard.TryGetObject(this.character, out IAtomicObject character))
                character.InvokeAction(ObjectAPI.MoveRequest, Vector3.zero);
        }
    }
}