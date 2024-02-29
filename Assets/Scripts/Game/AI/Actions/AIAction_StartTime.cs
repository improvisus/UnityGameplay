using AIModule;
using UnityEngine;

namespace Game.AI.Actions
{
    [CreateAssetMenu(
        fileName = "AIAction_StartTime",
        menuName = "Content/AI/Action/New AIAction_StartTime"
    )]
    public class AIAction_StartTime : AIAction
    {
        public override void Perform(IBlackboard blackboard)
        {
            blackboard.SetFloat(BlackboardAPI.StartTime, Time.time);
        }
    }
}