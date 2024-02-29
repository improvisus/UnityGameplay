using AIModule;
using UnityEngine;

namespace Game.AI.Conditions
{
    [CreateAssetMenu(
        fileName = "AICondition_Or",
        menuName = "Content/AI/Condition/New AICondition_Or"
    )]
    public class AICondition_Or : AICondition
    {
        [SerializeField]
        private AICondition[] conditions;
        
        public override bool Check(IBlackboard blackboard)
        {
            foreach (var condition in conditions)
            {
                if (condition.Check(blackboard))
                    return true;
            }

            return false;
        }
    }
}