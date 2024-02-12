using System;
using Atomic.Behaviours;
using Atomic.Elements;
using Atomic.Extensions;
using Game.Common;
using Game.Elements;
using UnityEngine;

namespace Game.Effects.Implementations
{
    [Serializable]
    public class DealDamagePeriodEffect : IEffect, IFixedUpdate
    {
        [SerializeField]
        private Countdown countdown;
        [SerializeField]
        private int damage;
        
        private IAtomicAction<int> damageAction;
        
        public virtual void Apply(AtomicBehaviour obj)
        {
            obj.AddLogic(this);
            
            if (obj.Is(ObjectType.Damageable))
                damageAction = obj.GetAction<int>(ObjectAPI.TakeDamageAction);
            
            countdown.Reset();
        }

        public virtual void Discard(AtomicBehaviour obj)
        {
            obj.RemoveLogic(this);
            countdown.Stop();
        }
        
        void IFixedUpdate.OnFixedUpdate(float deltaTime)
        {
            countdown.Update(deltaTime);
            
            if (countdown.CurrentTime <= 0)
            {
                damageAction?.Invoke(damage);
                countdown.Reset();
            }
        }
    }
}