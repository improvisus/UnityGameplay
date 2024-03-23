using AIModule;
using Atomic.Objects;
using Game.Engine;
using UnityEngine;

namespace Engine.AI.BTNodes
{
    public class BTNode_UnloadResources : BTNode
    {
        public override string Name => "Unload Resources";

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
            
            var resourceCapacity = sourceResourceStorage.Current;

            if (targetResourceStorage.FreeSlots < resourceCapacity)
                resourceCapacity = targetResourceStorage.FreeSlots;
            
            var canAddResources = targetResourceStorage.CanPutResources(resourceCapacity);
            if (canAddResources)
                targetResourceStorage.PutResources(sourceResourceStorage.ExtractAllResources());
            
            return BTState.SUCCESS;
        }
    }
}