using AIModule;
using UnityEngine;

namespace Game.AI.Conditions
{
    [CreateAssetMenu(
        fileName = "AICondition_Not",
        menuName = "Content/AI/Condition/New AICondition_Not"
    )]
    public class AICondition_Not : AICondition
    {
        [SerializeField]
        private AICondition conditions;
        
        public override bool Check(IBlackboard blackboard)
        {
            return !conditions.Check(blackboard);
        }
    }
}