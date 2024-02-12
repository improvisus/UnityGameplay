using Atomic.Behaviours;
using Atomic.Elements;
using Game.Common;
using UnityEngine;

namespace Game.Effects.Implementations
{
    [CreateAssetMenu(
        fileName = "BulletDamageEffect",
        menuName = "Content/Effect/New BulletDamageEffect"
    )]
    public sealed class BulletDamageEffect : ScriptableEffect
    {
        [SerializeField]
        private AtomicValue<int> sum;

        public override void Apply(AtomicBehaviour obj)
        {
            if (obj.TryGet(ObjectAPI.DamageExpression, out IAtomicExpression<int> damageExpression))
                damageExpression.Append(sum);
        }

        public override void Discard(AtomicBehaviour obj)
        {
            if (obj.TryGet(ObjectAPI.DamageExpression, out IAtomicExpression<int> damageExpression))
                damageExpression.Remove(sum);
        }
    }
}