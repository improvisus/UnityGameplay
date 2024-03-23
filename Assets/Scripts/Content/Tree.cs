using Atomic.Objects;
using Game.Engine;
using UnityEngine;

namespace Sample
{
    public sealed class Tree : AtomicEntity
    {
        private static readonly int ChopAnimHash = Animator.StringToHash("Chop"); 
        
        [SerializeField]
        private Transform transform;
        
        [SerializeField]
        private ResouceStorage resouceStorage;
        
        [SerializeField]
        private Animator animator;

        private void Awake()
        {
            this.AddType(ObjectType.Resource);
            this.AddProperty(ObjectAPI.ResourceStorage, this.resouceStorage);
            this.AddProperty(ObjectAPI.Transform, this.transform);
        }

        private void OnEnable()
        {
            this.resouceStorage.OnStateChanged += this.OnStateChanged;
        }

        private void OnDisable()
        {
            this.resouceStorage.OnStateChanged -= this.OnStateChanged;
        }

        private void OnStateChanged()
        {
            if (this.resouceStorage.IsEmpty())
            {
                this.gameObject.SetActive(false);
            }
            else
            {
                this.animator.Play(ChopAnimHash, -1, 0);
            }
        }
    }
}
