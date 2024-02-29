using AIModule;
using UnityEngine;

namespace Game.AI.Actions
{
    [CreateAssetMenu(
        fileName = "AIAction_AssignMoveToPosition",
        menuName = "Content/AI/Action/New AIAction_AssignMoveToPosition"
    )]
    public class AIAction_AssignMoveToPosition : AIAction
    {
        [SerializeField, BlackboardKey]
        private ushort moveToPosition;
        
        [SerializeField, BlackboardKey]
        private ushort movePosition;
        
        public override void Perform(IBlackboard blackboard)
        {
            if (!blackboard.TryGetVector3(this.moveToPosition, out Vector3 moveToPosition))
                return;
            
            blackboard.SetVector3(movePosition, moveToPosition);
        }
    }
}