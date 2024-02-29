using System;
using System.Collections.Generic;
using Atomic.Elements;
using GameEngine;

namespace Game.Expressions
{
    [Serializable]
    public class IntSumExpression : AtomicExpression<int>
    {
        protected override int Invoke(IReadOnlyList<IAtomicValue<int>> members)
        {
            int result = 0;
            
            for (int i = 0, count = members.Count; i < count; i++)
            {
                IAtomicValue<int> member = members[i];
                result += member.Value;
            }

            return result;
        }
    }
}