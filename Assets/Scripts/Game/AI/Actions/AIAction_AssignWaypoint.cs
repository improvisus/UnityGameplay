using AIModule;
using UnityEngine;

namespace Game.AI.Mechanics
{
    [CreateAssetMenu(
        fileName = "AIAction_AssignWaypoint",
        menuName = "Content/AI/Action/New AIAction_AssignWaypoint"
    )]
    public class AIAction_AssignWaypoint : AIAction
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

            var movePosition = waypoints[waypointIndex].position;
            
            blackboard.SetVector3(BlackboardAPI.MovePosition, movePosition);
        }
    }
}