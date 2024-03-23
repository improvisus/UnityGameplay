using System;
using UnityEngine;

namespace AIModule
{
    [Serializable]
    public sealed class BTNodeDecorator : BTNode
    {
        public override string Name => this.node != null ? this.node.Name : "Undefined";
        
        [SerializeField, Space]
        private AIAction[] enableActions;

        [SerializeField, Space]
        private AIAction[] disableActions;

        [SerializeReference, Space]
        private BTNode node;

        protected override void OnEnable(IBlackboard blackboard)
        {
            for (int i = 0, count = this.enableActions.Length; i < count; i++)
            {
                AIAction action = this.enableActions[i];
                action.Perform(blackboard);
            }
        }

        protected override BTState OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            return this.node.Run(blackboard, deltaTime);
        }

        protected override void OnDisable(IBlackboard blackboard)
        {
            for (int i = 0, count = this.disableActions.Length; i < count; i++)
            {
                AIAction action = this.disableActions[i];
                action.Perform(blackboard);
            }
        }
    }
}