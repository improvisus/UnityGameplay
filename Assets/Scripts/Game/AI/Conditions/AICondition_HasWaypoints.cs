using AIModule;
using Game.Extensions;
using UnityEngine;

namespace Game.AI.Conditions
{
    [CreateAssetMenu(
        fileName = "AICondition_HasWaypoints",
        menuName = "Content/AI/Condition/New AICondition_HasWaypoints"
    )]
    public class AICondition_HasWaypoints : AICondition
    {
        [SerializeField, BlackboardKey]
        private ushort waypoints;

        [SerializeField]
        private bool not;
        
        public override bool Check(IBlackboard blackboard)
        {
            bool hasWaypoints = blackboard.TryGetObject(this.waypoints, out Transform[] waypoints) && waypoints.IsNotNull();

            if (not)
                hasWaypoints = !hasWaypoints;
            
            return hasWaypoints;
        }
    }
}