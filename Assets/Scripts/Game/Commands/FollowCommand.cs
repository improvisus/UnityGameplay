using AIModule;
using Atomic.Objects;
using UnityEngine;

namespace Commands
{
    public class FollowCommand : ICommand
    {
        private IBlackboard blackboard;
        private AtomicObject target;
        
        public FollowCommand(IBlackboard blackboard, AtomicObject target)
        {
            this.blackboard = blackboard;
            this.target = target;
        }
        
        public void Execute()
        {
            blackboard.SetBool(BlackboardAPI.FollowEnabled, true);
            blackboard.SetObject(BlackboardAPI.Target, target);
            Debug.Log("Execute FOLLOW command");
        }
        
        public void Undo()
        {
            blackboard.SetBool(BlackboardAPI.FollowEnabled, false);
            Debug.Log("Undo FOLLOW command");
        }
    }
}