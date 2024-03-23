using System;
using Atomic.Behaviours;
using Atomic.Elements;
using UnityEngine;

namespace Engine.Look
{
    [Serializable]
    public sealed class LookComponent : IFixedUpdate
    {
        public IAtomicVariable<Vector3> LookDirection => this.lookDirection;
        
        [SerializeField]
        private Transform transform;

        [SerializeField]
        private AtomicVariable<Vector3> lookDirection;

        public void OnFixedUpdate(float deltaTime)
        {
            if (this.lookDirection.Value != Vector3.zero)
            {
                this.transform.rotation = Quaternion.LookRotation(this.lookDirection.Value, Vector3.up);
            }
        }
    }
}