using System;
using System.Collections.Generic;
using Atomic.Elements;
using GameEngine;

namespace Game.Expressions
{
    [Serializable]
    public class AndExpression : AtomicExpression<bool>
    {
        protected override bool Invoke(IReadOnlyList<IAtomicValue<bool>> members)
        {
            foreach (var member in members)
            {
                if (!member.Value)
                    return false;
            }

            return true;
        }
    }
}