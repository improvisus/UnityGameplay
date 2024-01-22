using Atomic.Elements;
using UnityEngine;

namespace Game.Mechanics
{
    public class CollisionMechanics
    {
        private readonly IAtomicEvent<Collider> collisionEvent;

        public CollisionMechanics(IAtomicEvent<Collider> collisionEvent)
        {
            this.collisionEvent = collisionEvent;
        }
        public void OnTriggerEnter(Collider other)
        {
            collisionEvent?.Invoke(other);
        }
    }
}