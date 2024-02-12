using System;
using System.Threading.Tasks;
using Atomic.Behaviours;
using UnityEngine;

namespace Game.Effects.Implementations
{
    [Serializable]
    public class TemporaryAsyncEffect : ICompletableEffect
    {
        [SerializeField]
        private float countdownTime;
        
        private Action<IEffect> callback;
        
        public virtual void Apply(AtomicBehaviour obj)
        {
            StartTimer(countdownTime);
        }

        public virtual void Discard(AtomicBehaviour obj)
        {
        }

        void ICompletableEffect.SetCallback(Action<IEffect> callback)
        {
            this.callback = callback;
        }
        
        private async Task StartTimer(float milliseconds)
        {
            await Task.Delay(TimeSpan.FromSeconds(milliseconds));
            callback?.Invoke(this);
        }
    }
}