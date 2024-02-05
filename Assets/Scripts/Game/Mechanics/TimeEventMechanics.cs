using Atomic.Elements;
using Game.Elements;

namespace Game.Mechanics
{
    public class TimeEventMechanics
    {
        private readonly Countdown time;
        private readonly IAtomicEvent timeEvent;

        public TimeEventMechanics(Countdown time, IAtomicEvent timeEvent)
        {
            this.time = time;
            this.timeEvent = timeEvent;
        }

        public void FixedUpdate(float fixedDeltaTime)
        {
            time.Update(fixedDeltaTime);

            if (time.IsPlaying)
                return;
            
            timeEvent?.Invoke();
            time.Stop();
        }
    }
}