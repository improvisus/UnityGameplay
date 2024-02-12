using System;
using Atomic.Behaviours;
using UnityEngine;

namespace Game.Effects.Implementations
{
    [Serializable]
    public class DestroyableEffect : IEffect
    {
        [SerializeField]
        private GameObject gameObject;
        
        public void Apply(AtomicBehaviour obj)
        {
        }

        public void Discard(AtomicBehaviour obj)
        {
            UnityEngine.Object.Destroy(this.gameObject);
        }
    }
}