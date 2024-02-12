using System;
using Atomic.Behaviours;
using Game.Common;
using Game.Elements;
using UnityEngine;

namespace Game.Effects.Implementations
{
    [Serializable]
    public class HealthRegenerationEffect : ICompletableEffect, IFixedUpdate
    {
        [SerializeField]
        private float durationTime;
        [SerializeField]
        private int healthUnit;
        
        private Action<IEffect> callback;
        
        private HitPoints hitPoints;
        public Countdown countdownRegenerationUnit;
        
        public virtual void Apply(AtomicBehaviour obj)
        {
            obj.AddLogic(this);
            
            hitPoints = obj.Get<HitPoints>(ObjectAPI.HitPoints);
            
            if (hitPoints.IsFull)
            {
                callback?.Invoke(this);
                return;
            }
            
            var regenerationPercent = (float)(hitPoints.Max - hitPoints.Current) / hitPoints.Max;
            var healthUnitInPercent = (float)healthUnit / hitPoints.Max;
            var countUnitInFullRegeneration = regenerationPercent / healthUnitInPercent;
            
            countdownRegenerationUnit = new Countdown(durationTime / countUnitInFullRegeneration);
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
            countdownRegenerationUnit.Update(deltaTime);
            if (countdownRegenerationUnit.CurrentTime <= 0)
            {
                countdownRegenerationUnit.Reset();
                hitPoints.Current += healthUnit;
                
                if(hitPoints.IsFull)
                    callback?.Invoke(this);
            }
        }
    }
}