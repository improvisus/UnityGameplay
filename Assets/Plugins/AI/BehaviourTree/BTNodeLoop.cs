using System;
using UnityEngine;
namespace AIModule
{
    [Serializable]
    public class BTNodeLoop : BTNode
    {
        public override string Name => this.node != null ? this.node.Name : "Undefined";
        
        [Header("Condition")]
        [SerializeReference, Space]
        private IAICondition condition;
        
        [SerializeReference, Space]
        private BTNode node;

        protected override BTState OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if(!condition.Check(blackboard))
                return BTState.FAILURE;

            this.node.Run(blackboard, deltaTime);
            return BTState.RUNNING;
        }
    }
}