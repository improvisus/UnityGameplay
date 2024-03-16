using AIModule;
using Atomic.Extensions;
using Atomic.Objects;
using Game.Common;
using UnityEngine;
namespace Game.Objects.States
{
    public class PatrolState : IAIState
    {
        private Transform[] patrolPoints;

        public PatrolState(Transform[] patrolPoints)
        {
            this.patrolPoints = patrolPoints;
        }
        
        public void OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if (!blackboard.TryGetObject(BlackboardAPI.Character, out IAtomicObject character) ||
                !blackboard.TryGetFloat(BlackboardAPI.StoppingDistance, out float stoppingDistance))
            {
                return;
            }
            
            int patrolIndex = 0;
            if (!blackboard.HasKey(BlackboardAPI.PatrolIndex))
                blackboard.SetInt(BlackboardAPI.PatrolIndex, patrolIndex);
            else
                patrolIndex = blackboard.GetInt(BlackboardAPI.PatrolIndex);
            
            if (!character.TryGet(ObjectAPI.Transform, out Transform transform))
                return;
            
            var moveDirection = character.GetVariable<Vector3>(ObjectAPI.MoveDirection);
            var currentPosition = transform.position;
            var targetPosition = patrolPoints[patrolIndex].position;
            var distanceVector = targetPosition - currentPosition;

            if (distanceVector.sqrMagnitude <= stoppingDistance * stoppingDistance)
            {
                moveDirection.Value = Vector3.zero;
                patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
                blackboard.SetInt(BlackboardAPI.PatrolIndex, patrolIndex);
                return;
            }
            
            moveDirection.Value = Vector3.ClampMagnitude(distanceVector, 1);
        }
        
        public void OnStart(IBlackboard blackboard)
        {
            
        }
        
        public void OnStop(IBlackboard blackboard)
        {
            if (!blackboard.TryGetObject(BlackboardAPI.Character, out IAtomicObject character))
                return;
            
            var moveDirection = character.GetVariable<Vector3>(ObjectAPI.MoveDirection);
            moveDirection.Value = Vector3.zero;
        }
    }
}