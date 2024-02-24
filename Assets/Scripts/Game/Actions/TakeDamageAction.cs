using Atomic.Elements;
using Game.Elements;
using Sirenix.OdinInspector;

namespace Game.Actions
{
    public class TakeDamageAction : IAtomicAction<int>
    {
        private HitPoints hitPoints;
        private IAtomicValue<int> armor;
        
        public void Compose(HitPoints hitPoints, IAtomicValue<int> armor) 
        {
            this.hitPoints = hitPoints;
            this.armor = armor;
        }
        
        [Button]
        public void Invoke(int damage)
        {
            if (hitPoints.Current > 0)
            {
                var damageWithArmor = (int)(damage * (1f - armor.Value / 100f));
                hitPoints.Current -= damageWithArmor;
            }
        }
    }
}