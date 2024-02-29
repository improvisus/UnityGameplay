using AIModule;
using Atomic.Objects;
using Game.Extensions;
using UnityEngine;

namespace Game.AI.Actions
{
    [CreateAssetMenu(
        fileName = "AIAction_ObjectToObject",
        menuName = "Content/AI/Action/New AIAction_ObjectToObject"
    )]
    public class AIAction_ObjectToObject : AIAction
    {
        [SerializeField, BlackboardKey]
        private ushort source;

        [SerializeField, BlackboardKey]
        private ushort target;

        public override void Perform(IBlackboard blackboard)
        {
            if (!blackboard.TryGetObject(this.source, out IAtomicObject source) || source.IsNull())
                return;
            
            blackboard.SetObject(target, source);
        }
    }
}