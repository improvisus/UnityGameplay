using AIModule;
using UnityEngine;
namespace Game.AI.Actions
{
    [CreateAssetMenu(
        fileName = "AIAction_NextWaypoint",
        menuName = "Content/AI/Action/New AIAction_NextWaypoint"
    )]
    public class AIAction_NextWaypoint : AIAction
    {
        [SerializeField, BlackboardKey]
        private ushort waypoints;

        [SerializeField, BlackboardKey]
        private ushort waypointIndex;
        
        public override void Perform(IBlackboard blackboard)
        {
            if (!blackboard.TryGetObject(this.waypoints, out Transform[] waypoints) ||
                !blackboard.TryGetInt(this.waypointIndex, out int waypointIndex))
            {
                return;
            }

            waypointIndex++;
            waypointIndex %= waypoints.Length;

            blackboard.SetInt(this.waypointIndex, waypointIndex);
        }
    }
}