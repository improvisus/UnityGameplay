using Atomic.Behaviours;
using Atomic.Elements;
using Game.Common;
using UnityEngine;

namespace Game.Effects.Implementations
{
    [CreateAssetMenu(
        fileName = "ArmorEffect",
        menuName = "Content/Effect/New ArmorEffect"
    )]
    public sealed class ArmorEffect : ScriptableEffect
    {
        [SerializeField]
        private int percentage;

        public override void Apply(AtomicBehaviour obj)
        {
            if (obj.TryGet(ObjectAPI.Armor, out IAtomicVariable<int> armor))
                armor.Value = percentage;
        }

        public override void Discard(AtomicBehaviour obj)
        {
            if (obj.TryGet(ObjectAPI.Armor, out IAtomicVariable<int> armor))
                armor.Value = 0;
        }
    }
}