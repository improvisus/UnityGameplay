using System;
using AIModule;
using Atomic.Objects;
using Game.Extensions;
using UnityEngine;

namespace Game.AI.Conditions
{
    [CreateAssetMenu(
        fileName = "AICondition_HasTarget",
        menuName = "Content/AI/Condition/New AICondition_HasTarget"
    )]
    public class AICondition_HasTarget : AICondition
    {
        [SerializeField, BlackboardKey]
        private ushort target;

        [SerializeField]
        private bool not;
        
        public override bool Check(IBlackboard blackboard)
        {
            bool hasTarget = blackboard.TryGetObject(this.target, out IAtomicObject target) && target.IsNotNull();

            if (not)
                hasTarget = !hasTarget;
            
            return hasTarget;
        }
    }
}