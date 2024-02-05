using Atomic.Behaviours;
using Atomic.Elements;

namespace Game.Objects
{
    public abstract class Weapon : AtomicBehaviour
    {
        public abstract IAtomicValue<bool> CanAttack { get; }
        
        public abstract IAtomicAction AttackAction { get; }
    }
}