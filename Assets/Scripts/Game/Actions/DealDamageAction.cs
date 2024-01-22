using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using Game.Common;

namespace Game.Actions
{
    public class DealDamageAction : IAtomicAction<IAtomicObject>
    {
        private IAtomicValue<int> damage;
  
        public void Compose(IAtomicValue<int> damage) 
        {
            this.damage = damage;
        }
        
        public void Invoke(IAtomicObject target)
        {
            if (!target.Is(ObjectAPI.Damageable))
                return;
                
            var damageAction = target.GetAction<int>(ObjectAPI.TakeDamageAction);
            damageAction?.Invoke(damage.Value);
        }
    }
}
