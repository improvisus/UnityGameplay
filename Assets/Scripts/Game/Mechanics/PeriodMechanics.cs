using Atomic.Elements;
using Game.Elements;

namespace Game.Mechanics
{
    public class PeriodMechanics
    {
        private readonly IAtomicAction actionPeriod;
        private readonly Countdown timePeriod;
        
        public PeriodMechanics(Countdown timePeriod, IAtomicAction actionPeriod)
        {
            this.actionPeriod = actionPeriod;
            this.timePeriod = timePeriod;
        }

        public void Start()
        {
            //timePeriod.Reset();
        }
        
        public void FixedUpdate(float fixedDeltaTime)
        {
            timePeriod.Update(fixedDeltaTime);

            if (timePeriod.IsFinished)
            {
                actionPeriod?.Invoke();
                timePeriod.Reset();
            }
        }
    }
}