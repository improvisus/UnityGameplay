using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AIModule
{
    [Serializable]
    public abstract class BTNode
    {
        public virtual string Name => this.GetType().Name;

        [ShowInInspector, ReadOnly]
        private bool enable;

        public BTState Run(IBlackboard blackboard, float deltaTime)
        {
            if (!this.enable)
            {
                this.enable = true;
                this.OnEnable(blackboard);
            }

            BTState result = this.OnUpdate(blackboard, deltaTime);

            //Debug.LogError(Name);
            if (result != BTState.RUNNING)
            {
                this.enable = false;
                this.OnDisable(blackboard);
            }

            return result;
        }

        public void Abort(IBlackboard blackboard)
        {
            if (this.enable)
            {
                this.enable = false;
                this.OnAbort(blackboard);
                this.OnDisable(blackboard);
            }
        }
        
        protected abstract BTState OnUpdate(IBlackboard blackboard, float deltaTime);
        
        protected virtual void OnEnable(IBlackboard blackboard)
        {
        }

        protected virtual void OnDisable(IBlackboard blackboard)
        {
        }

        protected virtual void OnAbort(IBlackboard blackboard)
        {
        }
    }
}