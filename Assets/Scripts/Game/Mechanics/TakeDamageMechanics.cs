using Atomic.Elements;
using Game.Elements;

namespace Game.Mechanics
{
    public class TakeDamageMechanics
    {
        private readonly HitPoints hitPoints;
        private readonly IAtomicObservable<int> takeDamageEvent;

        public TakeDamageMechanics(HitPoints hitPoints, IAtomicObservable<int> takeDamageEvent)
        {
            this.hitPoints = hitPoints;
            this.takeDamageEvent = takeDamageEvent;
        }
        
        public void OnEnable()
        {
            takeDamageEvent.Subscribe(OnTakeDamage);
        }

        public void OnDisable()
        {
            takeDamageEvent.Unsubscribe(OnTakeDamage);
        }
        
        private void OnTakeDamage(int damage)
        {
            if (hitPoints.Current > 0)
                hitPoints.Current -= damage;
        }
    }
}