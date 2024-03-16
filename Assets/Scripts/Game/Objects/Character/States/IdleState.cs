using AIModule;
using Atomic.Extensions;
using Atomic.Objects;
using Game.Common;
using UnityEngine;

namespace Game.Objects.States
{
    public class IdleState : IAIState
    {

        public void OnStart(IBlackboard blackboard)
        {
            if (!blackboard.TryGetObject(BlackboardAPI.Character, out IAtomicObject character))
                return;
            
            var moveDirection = character.GetVariable<Vector3>(ObjectAPI.MoveDirection);
            moveDirection.Value = Vector3.zero;
        }
        public void OnStop(IBlackboard blackboard)
        {
        }
        public void OnUpdate(IBlackboard blackboard, float deltaTime)
        {
        }
    }
}