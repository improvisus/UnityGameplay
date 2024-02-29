using AIModule;
using Atomic.Extensions;
using Atomic.Objects;
using Game.Common;
using UnityEngine;

namespace Game.AI.Mechanics
{
    [CreateAssetMenu(
        fileName = "AIMoveToPositionMechanics",
        menuName = "Content/AI/Mechanics/New AIMoveToPositionMechanics"
    )]
    public class AIMoveToPositionMechanics : AIMechanics
    {
        [SerializeField, BlackboardKey]
        private ushort movePosition;
        
        [SerializeField, BlackboardKey]
        private ushort character;
        
        public override void OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if (!blackboard.TryGetVector3(this.movePosition, out Vector3 movePosition) ||
                !blackboard.TryGetObject(this.character, out IAtomicObject character))
            {
                return;
            }
            
            if (!character.TryGet(ObjectAPI.Transform, out Transform transform))
                return;
            
            var moveDirection = character.GetVariable<Vector3>(ObjectAPI.MoveDirection);
            var currentPosition = transform.position;
            var targetPosition = movePosition;
            var distanceVector = targetPosition - currentPosition;
            
            moveDirection.Value = Vector3.ClampMagnitude(distanceVector, 1);
        }
    }
}