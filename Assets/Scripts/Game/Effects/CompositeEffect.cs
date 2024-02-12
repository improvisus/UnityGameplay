using System;
using Atomic.Behaviours;
using UnityEngine;

namespace Game.Effects
{
    public sealed class CompositeEffect : MonoBehaviour, ICompletableEffect
    {
        [SerializeField]
        private ScriptableEffect[] scriptableEffects;

        [SerializeReference]
        private IEffect[] plainEffects;

        private Action<IEffect> callback;

        private void Awake()
        {
            foreach (var effect in plainEffects)
            {
                if (effect is ICompletableEffect completable)
                    completable.SetCallback(OnComplete);
            }
        }

        private void OnComplete(IEffect _)
        {
            callback.Invoke(this);
        }

        public void Apply(AtomicBehaviour obj)
        {
            foreach (var effect in scriptableEffects)
                effect.Apply(obj);

            foreach (var effect in plainEffects)
                effect.Apply(obj);
        }

        public void Discard(AtomicBehaviour obj)
        {
            foreach (var effect in scriptableEffects)
                effect.Discard(obj);
            
            foreach (var effect in plainEffects)
                effect.Discard(obj);
        }

        void ICompletableEffect.SetCallback(Action<IEffect> callback)
        {
            this.callback = callback;
        }
    }
}