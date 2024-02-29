using AIModule;
using Atomic.Objects;
using Game.Common;
using UnityEngine;
namespace Game.AI.Actions
{
    [CreateAssetMenu(
        fileName = "AIAction_AssignMovePosition",
        menuName = "Content/AI/Action/New AIAction_AssignMovePosition"
    )]
    public class AIAction_AssignMovePosition : AIAction
    {
        [SerializeField, BlackboardKey]
        private ushort target;
        
        [SerializeField, BlackboardKey]
        private ushort movePosition;
        
        public override void Perform(IBlackboard blackboard)
        {
            if (!blackboard.TryGetObject(this.target, out IAtomicObject target))
                return;

            if(target == null)
                return;
            
            if (!target.TryGet(ObjectAPI.Transform, out Transform targetTransform))
                return;
            
            if(targetTransform == null)
                return;
            
            blackboard.SetVector3(movePosition, targetTransform.position);
        }
    }
}