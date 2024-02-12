using Atomic.Behaviours;
using Atomic.Elements;
using Game.Common;
using UnityEngine;

namespace Game.Effects.Implementations
{
    [CreateAssetMenu(
        fileName = "MoveSpeedEffect",
        menuName = "Content/Effect/New MoveSpeedEffect"
    )]
    public sealed class MoveSpeedEffect : ScriptableEffect
    {
        [SerializeField]
        private AtomicValue<float> multiplier;

        public override void Apply(AtomicBehaviour obj)
        {
            if (obj.TryGet(ObjectAPI.MoveSpeedExpression, out IAtomicExpression<float> moveSpeedExpression))
                moveSpeedExpression.Append(this.multiplier);
        }

        public override void Discard(AtomicBehaviour obj)
        {
            if (obj.TryGet(ObjectAPI.MoveSpeedExpression, out IAtomicExpression<float> moveSpeedExpression))
                moveSpeedExpression.Remove(this.multiplier);
        }
    }
}