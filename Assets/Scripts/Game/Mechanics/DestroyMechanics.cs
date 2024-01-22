using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace Game.Mechanics
{
    public class DestroyMechanics
    {
        private readonly IAtomicEvent destroyEvent;
        private readonly AtomicObject destroyObject;

        public DestroyMechanics(IAtomicEvent destroyEvent, AtomicObject destroyObject)
        {
            this.destroyEvent = destroyEvent;
            this.destroyObject = destroyObject;
        }

        public void OnEnable()
        {
            destroyEvent.Subscribe(OnDestroy);
        }

        public void OnDisable()
        {
            destroyEvent.Unsubscribe(OnDestroy);
        }

        private void OnDestroy()
        {
            Object.Destroy(destroyObject.gameObject);
        }
    }
}