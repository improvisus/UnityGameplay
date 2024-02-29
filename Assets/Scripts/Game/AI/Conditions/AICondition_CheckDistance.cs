using AIModule;
using Atomic.Objects;
using Game.Common;
using UnityEngine;

namespace Game.AI.Conditions
{
    [CreateAssetMenu(
        fileName = "AICondition_CheckDistance",
        menuName = "Content/AI/Condition/New AICondition_CheckDistance"
    )]
    public class AICondition_CheckDistance : AICondition
    {
        [SerializeField, BlackboardKey]
        private ushort targetPosition;
        
        [SerializeField, BlackboardKey]
        private ushort character;
        
        [SerializeField]
        private float distance;
        
        public override bool Check(IBlackboard blackboard)
        {
            if (!blackboard.TryGetVector3(this.targetPosition, out Vector3 targetPosition) ||
                !blackboard.TryGetObject(this.character, out IAtomicObject character))
            {
                return false;
            }

            if (!character.TryGet(ObjectAPI.Transform, out Transform transform))
                return false;
            
            var currentPosition = transform.position;
            var distanceVector = targetPosition - currentPosition;

            return distanceVector.sqrMagnitude <= distance * distance;
        }
    }
}