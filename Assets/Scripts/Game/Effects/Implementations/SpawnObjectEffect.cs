using System;
using Atomic.Behaviours;
using UnityEngine;

namespace Game.Effects.Implementations
{
    [Serializable]
    public class SpawnObjectEffect : IEffect
    {
        [SerializeField]
        private GameObject prefab;
        [SerializeField]
        private bool inSideObject = true;

        private GameObject gameObject;
        
        public void Apply(AtomicBehaviour obj)
        {
            var transform = obj.transform;
            gameObject = UnityEngine.Object.Instantiate(prefab, transform.position, transform.rotation, inSideObject ? transform : null);
        }

        public void Discard(AtomicBehaviour obj)
        {
            UnityEngine.Object.Destroy(gameObject);
        }
    }
}