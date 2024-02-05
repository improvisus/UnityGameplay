using System;

namespace Game.Mechanics
{
    public class FixedUpdateMechanics
    {
        private readonly Action<float> action;

        public FixedUpdateMechanics(Action<float> action)
        {
            this.action = action;
        }

        public void FixedUpdate(float fixedDeltaTime)
        {
            action.Invoke(fixedDeltaTime);
        }
    }
}