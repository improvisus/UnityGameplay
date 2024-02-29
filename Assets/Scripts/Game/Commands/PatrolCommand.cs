using AIModule;
using UnityEngine;
namespace Commands
{
    public class PatrolCommand : ICommand
    {
        private IBlackboard blackboard;
        private Transform[] waypoints;
        
        public PatrolCommand(IBlackboard blackboard, Transform[] waypoints)
        {
            this.blackboard = blackboard;
            this.waypoints = waypoints;
        }
        
        public void Execute()
        {
            blackboard.SetBool(BlackboardAPI.PatrolEnabled, true);
            blackboard.SetObject(BlackboardAPI.Waypoints, waypoints);
            blackboard.SetInt(BlackboardAPI.WaypointIndex, 0);
            Debug.Log("Execute PATROL command");
        }
        
        public void Undo()
        {
            blackboard.SetBool(BlackboardAPI.PatrolEnabled, false);
            Debug.Log("Undo PATROL command");
        }
    }
}