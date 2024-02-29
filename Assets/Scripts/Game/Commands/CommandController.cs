using AIModule;
using Atomic.Objects;
using Game.Common;
using UnityEngine;

namespace Commands
{
    public sealed class CommandController : MonoBehaviour
    {
        [SerializeField]
        private AtomicObject character;
        [SerializeField]
        private AtomicObject target;
        [SerializeField]
        private Transform[] waypoints;
        
        private ICommand currentCommand;
        
        private IBlackboard blackboard;

        private Transform transformGround;
        
        private void Start()
        {
            blackboard = character.Get<IBlackboard>(ObjectAPI.Blackboard);
            transformGround = character.Get<Transform>(ObjectAPI.Transform);
        }
        
        private void Update()
        {
            if (Input.GetKey(KeyCode.S))
            {
                currentCommand?.Undo();
                currentCommand = null;
            }
            
            if (!Input.GetMouseButtonDown(0))
                return;

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var groundPlane = new Plane(Vector3.up, transformGround.position);
            
            if (!groundPlane.Raycast(ray, out float rayLength))
                return;
            
            var pointClick = ray.GetPoint(rayLength);
            
            if (Input.GetKey(KeyCode.M))
                currentCommand = new MoveToPositionCommand(blackboard, pointClick);
            else if (Input.GetKey(KeyCode.P))
                currentCommand = new PatrolCommand(blackboard, waypoints);
            else if (Input.GetKey(KeyCode.A))
                currentCommand = new AttackCommand(blackboard, target);
            else if (Input.GetKey(KeyCode.F))
                currentCommand = new FollowCommand(blackboard, target);

            currentCommand?.Execute();
        }
    }
}