using Atomic.Elements;
using Game.Elements;

namespace Game.Mechanics
{
    public class LifetimeMechanics
    {
        private readonly Countdown lifetime;
        private readonly IAtomicEvent deathEvent;

        public LifetimeMechanics(Countdown lifetime, IAtomicEvent deathEvent)
        {
            this.lifetime = lifetime;
            this.deathEvent = deathEvent;
        }

        public void FixedUpdate(float fixedDeltaTime)
        {
            lifetime.Update(fixedDeltaTime);

            if (lifetime.IsPlaying)
                return;
            
            deathEvent.Invoke();
        }
    }
}