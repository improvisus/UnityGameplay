using System;
using System.Collections.Generic;
using Atomic.Behaviours;
using Sirenix.OdinInspector;

namespace Game.Effects
{
    [Serializable]
    public sealed class EffectManager
    {
        public event Action<IEffect> OnEffectAdded;
        public event Action<IEffect> OnEffectRemoved; 

        public IReadOnlyList<IEffect> Effects => effects;

        [ShowInInspector, ReadOnly]
        private readonly List<IEffect> effects = new();
        
        private AtomicBehaviour owner;

        public EffectManager()
        {
        }

        public EffectManager(AtomicBehaviour owner)
        {
            this.owner = owner;
        }

        public void Compose(AtomicBehaviour owner)
        {
            this.owner = owner;
        }
        
        [Button]
        public void ApplyEffect(IEffect effect)
        {
            if (effect is ICompletableEffect completable)
                completable.SetCallback(DiscardEffect);
            
            effects.Add(effect);
            
            effect.Apply(owner);
            OnEffectAdded?.Invoke(effect);
        }

        [Button]
        public void DiscardEffect(IEffect effect)
        {
            if (effects.Remove(effect))
            {
                effect.Discard(owner);
                OnEffectRemoved?.Invoke(effect);
            }
        }

        [Button]
        public void DiscardAllEffects()
        {
            foreach (var effect in effects)
                effect.Discard(owner);
            
            effects.Clear();
        }
    }
}