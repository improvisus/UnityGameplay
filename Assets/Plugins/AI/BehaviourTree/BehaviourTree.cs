using System;
using UnityEngine;

namespace AIModule
{
    [Serializable]
    public sealed class BehaviourTree : IAIUpdatable, IAIStoppable
    {
        [SerializeReference]
        private BTNode root;
        
        public void OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            this.root.Run(blackboard, deltaTime);
        }

        public void OnStop(IBlackboard blackboard)
        {
            this.root.Abort(blackboard);
        }
    }
}