using AIModule;
using Atomic.Extensions;
using Atomic.Objects;
using Game.Common;
using UnityEngine;
namespace Game.AI.Actions
{
    [CreateAssetMenu(
        fileName = "AIAction_StopMove",
        menuName = "Content/AI/Action/New AIAction_StopMove"
    )]
    public class AIAction_StopMove : AIAction
    {
        [SerializeField, BlackboardKey]
        private ushort character;
        
        public override void Perform(IBlackboard blackboard)
        {
            if (!blackboard.TryGetObject(this.character, out IAtomicObject character))
            {
                return;
            }

            if (character.TryGetVariable<Vector3>(ObjectAPI.MoveDirection, out var moveDirection))
                moveDirection.Value = Vector3.zero;
        }
    }
}