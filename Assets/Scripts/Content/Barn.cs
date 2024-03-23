using Atomic.Objects;
using Game.Engine;
using UnityEngine;

namespace Sample
{
    [Is(ObjectType.Barn)]
    public sealed class Barn : AtomicEntity
    {
        [SerializeField]
        private Transform transform;
        
        [SerializeField]
        private ResouceStorage storage;

        private void Awake()
        {
            this.AddType(ObjectType.Barn);
            this.AddProperty(ObjectAPI.ResourceStorage, this.storage);
            this.AddProperty(ObjectAPI.Transform, this.transform);
        }
    }
}