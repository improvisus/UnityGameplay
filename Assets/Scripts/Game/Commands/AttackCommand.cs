using AIModule;
using Atomic.Objects;
using UnityEngine;
namespace Commands
{
    public class AttackCommand : ICommand
    {
        private IBlackboard blackboard;
        private AtomicObject target;
        
        public AttackCommand(IBlackboard blackboard, AtomicObject target)
        {
            this.blackboard = blackboard;
            this.target = target;
        }
        
        public void Execute()
        {
            blackboard.SetBool(BlackboardAPI.AttackEnabled, true);
            blackboard.SetObject(BlackboardAPI.Target, target);
            Debug.Log("Execute ATTACK command");
        }
        
        public void Undo()
        {
            blackboard.SetBool(BlackboardAPI.AttackEnabled, false);
            //blackboard.DeleteObject(BlackboardAPI.AttackTarget);
            Debug.Log("Undo ATTACK command");
        }
    }
}