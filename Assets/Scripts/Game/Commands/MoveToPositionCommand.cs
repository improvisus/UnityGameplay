using AIModule;
using UnityEngine;

namespace Commands
{
    public class MoveToPositionCommand : ICommand
    {
        private IBlackboard blackboard;
        private Vector3 pointClick;
        
        public MoveToPositionCommand(IBlackboard blackboard, Vector3 pointClick)
        {
            this.blackboard = blackboard;
            this.pointClick = pointClick;
        }
        
        public void Execute()
        {
            blackboard.SetBool(BlackboardAPI.MoveToPositionEnabled, true);
            blackboard.SetVector3(BlackboardAPI.MoveToPosition, pointClick);
            Debug.Log("Execute MOVE_TO_POSITION command");
        }
        
        public void Undo()
        {
            blackboard.SetBool(BlackboardAPI.MoveToPositionEnabled, false);
            Debug.Log("Undo MOVE_TO_POSITION command");
        }
    }
}