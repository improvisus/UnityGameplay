using AIModule;
using UnityEngine;
namespace Game.AI.Conditions
{
    [CreateAssetMenu(
        fileName = "AICondition_And",
        menuName = "Content/AI/Condition/New AICondition_And"
    )]
    public class AICondition_And : AICondition
    {
        [SerializeField]
        private AICondition[] conditions;

        public override bool Check(IBlackboard blackboard)
        {
            foreach (var condition in conditions)
            {
                if (!condition.Check(blackboard))
                    return false;
            }

            return true;
        }
    }
}