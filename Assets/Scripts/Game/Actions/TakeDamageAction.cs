using System;
using Atomic.Elements;
using Game.Elements;
using Sirenix.OdinInspector;

namespace Game.Actions
{
    [Serializable]
    public class TakeDamageAction : IAtomicAction<int>
    {
        private HitPoints hitPoints;
        
        public void Compose(HitPoints hitPoints) 
        {
            this.hitPoints = hitPoints;
        }
        
        [Button]
        public void Invoke(int damage)
        {
            if (hitPoints.Current > 0)
                hitPoints.Current -= damage;
        }
    }
}