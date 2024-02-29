using AIModule;
using Atomic.Objects;
using Game.Common;
using Game.Extensions;
using UnityEngine;
namespace Game.AI.Conditions
{
    [CreateAssetMenu(
        fileName = "AICondition_EqualsVector3AndTarget",
        menuName = "Content/AI/Condition/New AICondition_EqualsVector3AndTarget"
    )]
    public class AICondition_EqualsVector3AndTarget: AICondition
    {
        [SerializeField, BlackboardKey]
        private ushort vector;
        
        [SerializeField, BlackboardKey]
        private ushort target;
        
        [SerializeField]
        private bool not;
        
        public override bool Check(IBlackboard blackboard)
        {
            if (!blackboard.TryGetVector3(this.vector, out Vector3 vector) ||
                !blackboard.TryGetObject(this.target, out IAtomicObject target) || target.IsNull())
            {
                return false;
            }

            if (!target.TryGet(ObjectAPI.Transform, out Transform targetTransform))
                return false;

            return not ? vector != targetTransform.position : vector == targetTransform.position;
        }
    }
}