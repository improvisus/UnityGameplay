using System;
using Atomic.Behaviours;
using Game.Elements;
using UnityEngine;

namespace Game.Effects.Implementations
{
    [Serializable]
    public class TemporaryEffect : ICompletableEffect, IFixedUpdate
    {
        [SerializeField]
        private Countdown countdown;

        private Action<IEffect> callback;
        
        public virtual void Apply(AtomicBehaviour obj)
        {
            obj.AddLogic(this);
            countdown.Reset();
        }

        public virtual void Discard(AtomicBehaviour obj)
        {
            obj.RemoveLogic(this);
        }

        void ICompletableEffect.SetCallback(Action<IEffect> callback)
        {
            this.callback = callback;
        }
        
        void IFixedUpdate.OnFixedUpdate(float deltaTime)
        {
            countdown.Update(deltaTime);
            
            if (countdown.CurrentTime <= 0)
                callback?.Invoke(this);
        }
    }
}