using Atomic.Behaviours;
using UnityEngine;

namespace Game.Effects
{
    public abstract class ScriptableEffect : ScriptableObject, IEffect
    {
        public abstract void Apply(AtomicBehaviour obj);

        public abstract void Discard(AtomicBehaviour obj);
    }
}