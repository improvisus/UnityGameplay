using AIModule;
using Atomic.Extensions;
using Atomic.Objects;
using Game.Engine;
using UnityEngine;

namespace Engine.AI.BTNodes
{
    public class BTNode_GatherResources : BTNode
    {
        public override string Name => "Gather Resources";

        [SerializeField, BlackboardKey]
        private ushort sourceObject;

        [SerializeField, BlackboardKey]
        private ushort targetObject;
        
        protected override BTState OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if (!blackboard.TryGetObject(this.sourceObject, out IAtomicObject sourceObject) ||
                !blackboard.TryGetObject(this.targetObject, out IAtomicObject targetObject))
            {
                return BTState.FAILURE;
            }
            
            if (!sourceObject.TryGet(ObjectAPI.ResourceStorage, out ResouceStorage sourceResourceStorage) ||
                !targetObject.TryGet(ObjectAPI.ResourceStorage, out ResouceStorage targetResourceStorage))
            {
                return BTState.FAILURE;
            }
            
            if(sourceResourceStorage.IsFull())
                return BTState.FAILURE;
            
            if(targetResourceStorage.IsEmpty())
                return BTState.FAILURE;
            
            if(sourceObject.GetValue<bool>(ObjectAPI.IsChopping).Value)
                return BTState.FAILURE;

            sourceObject.InvokeAction(ObjectAPI.GatherRequest);
            
            return BTState.SUCCESS;
        }
    }
}