using AIModule;
using Atomic.Objects;
using UnityEngine;

namespace Game.Engine.AI.Conditions
{
    [CreateAssetMenu(
        fileName = "AICondition_ResourceStorageIsFull",
        menuName = "Engine/AI/New AICondition_ResourceStorageIsFull"
    )]
    public class AICondition_ResourceStorageIsFull : AICondition
    {
        [SerializeField, BlackboardKey]
        private ushort atomicObject;
        
        [SerializeField]
        private bool not;
        
        public override bool Check(IBlackboard blackboard)
        {
            if (!blackboard.TryGetObject(this.atomicObject, out IAtomicObject atomicObject) ||
                !atomicObject.TryGet(ObjectAPI.ResourceStorage, out ResouceStorage resourceStorage))
                return false;

            bool isFull = resourceStorage.IsFull();
            
            if (not)
                isFull = !isFull;

            //Debug.LogError(isFull);
            return isFull;
        }
    }
}