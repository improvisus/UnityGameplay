using AIModule;
using Atomic.Objects;
using UnityEngine;
namespace Game.Engine.AI.AIActions
{
    [CreateAssetMenu(
        fileName = "AIAction_UnloadResources",
        menuName = "Engine/AI/New AIAction_UnloadResources"
    )]
    public class AIAction_UnloadResources : AIAction
    {
        [SerializeField, BlackboardKey]
        private ushort sourceObject;

        [SerializeField, BlackboardKey]
        private ushort targetObject;

        public override void Perform(IBlackboard blackboard)
        {
            if (!blackboard.TryGetObject(this.sourceObject, out IAtomicObject sourceObject) ||
                !blackboard.TryGetObject(this.targetObject, out IAtomicObject targetObject))
            {
                return;
            }
            
            if (!sourceObject.TryGet(ObjectAPI.ResourceStorage, out ResouceStorage sourceResourceStorage) ||
                !targetObject.TryGet(ObjectAPI.ResourceStorage, out ResouceStorage targetResourceStorage))
            {
                return;
            }
            
            if(targetResourceStorage.IsFull())
                return;
            
            var resourceCapacity = sourceResourceStorage.Current;

            var canAddResources = targetResourceStorage.CanPutResources(resourceCapacity);
            if (canAddResources)
                targetResourceStorage.PutResources(sourceResourceStorage.ExtractAllResources());
        }
    }
}