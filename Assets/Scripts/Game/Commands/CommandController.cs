using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using Game.Common;
using Game.Objects.States;
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
        
        private Transform transformGround;

        private StateCommands<StateCommand> stateCommands;
        
        private IAtomicValue<IAtomicObject> foundTarget;
        
        private void Start()
        {
            transformGround = character.Get<Transform>(ObjectAPI.Transform);
            stateCommands = character.Get<StateCommands<StateCommand>>(ObjectAPI.StateCommands);
            foundTarget = character.GetValue<IAtomicObject>(ObjectAPI.FoundTarget);

            stateCommands.Execute(StateCommand.Idle, new IdleState());
        }
        
        private void Update()
        {
            UpdateFindTarget();

            UpdateInput();
        }
        
        private void UpdateInput()
        {
            if (Input.GetKeyDown(KeyCode.S))
                stateCommands.Undo();

            if (!Input.GetMouseButtonDown(0))
                return;

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var groundPlane = new Plane(Vector3.up, transformGround.position);

            if (!groundPlane.Raycast(ray, out float rayLength))
                return;

            var pointClick = ray.GetPoint(rayLength);

            if (Input.GetKey(KeyCode.M))
                stateCommands.Execute(StateCommand.MoveToPoint, new MoveToPositionState(pointClick));
            else if (Input.GetKey(KeyCode.P))
                stateCommands.Execute(StateCommand.Patrol, new PatrolState(waypoints));
            else if (Input.GetKey(KeyCode.A))
                stateCommands.Execute(StateCommand.Attack, new AttackState(target));
            else if (Input.GetKey(KeyCode.F))
                stateCommands.Execute(StateCommand.MoveToTarget, new MoveToTargetState(target));
        }

        private void UpdateFindTarget()
        {
            if (stateCommands.currentKey == StateCommand.Attack && foundTarget.Value == null)
                stateCommands.Undo();

            if (stateCommands.currentKey != StateCommand.Attack && foundTarget.Value != null)
                stateCommands.Execute(StateCommand.Attack, new AttackState(foundTarget.Value));
        }
    }
}