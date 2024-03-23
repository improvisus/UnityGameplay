using System;
using AIModule;
using UnityEngine;

namespace Engine.AI.BTNodes
{
    [Serializable]
    public class BTNode_EmptyRunning : BTNode
    {
        public override string Name => "Empty Running";
        
        protected override BTState OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            return BTState.RUNNING;
        }
    }
}