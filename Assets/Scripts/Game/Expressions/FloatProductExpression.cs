using System;
using System.Collections.Generic;
using Atomic.Elements;
using Game.Engine;

namespace Game.Expressions
{
    [Serializable]
    public sealed class FloatProductExpression : AtomicExpression<float>
    {
        protected override float Invoke(IReadOnlyList<IAtomicValue<float>> members)
        {
            float result = members.Count == 0 ? 0 : 1;
            
            for (int i = 0, count = members.Count; i < count; i++)
            {
                IAtomicValue<float> member = members[i];
                result *= member.Value;
            }

            return result;
        }
    }
}