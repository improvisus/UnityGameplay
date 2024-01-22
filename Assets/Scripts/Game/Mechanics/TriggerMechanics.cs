using Atomic.Elements;
using Atomic.Objects;

namespace Game.Mechanics
{
    public class TriggerMechanics
    {
        private readonly IAtomicAction<IAtomicObject> collisionAction;
        private readonly IAtomicAction collisionEvent;
        
        public TriggerMechanics(IAtomicAction<IAtomicObject> collisionAction, IAtomicAction collisionEvent)
        {
            this.collisionAction = collisionAction;
            this.collisionEvent = collisionEvent;
        }
        
        public void OnTriggerEnter(IAtomicObject other)
        {
            collisionAction?.Invoke(other);
            collisionEvent?.Invoke();
        }
    }
}