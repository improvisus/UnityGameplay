using Atomic.Behaviours;

namespace Game.Effects.Implementations
{
    public class HideObjectEffect : IEffect
    {
        public void Apply(AtomicBehaviour obj)
        {
            obj.gameObject.SetActive(false);
        }

        public void Discard(AtomicBehaviour obj)
        {
            obj.gameObject.SetActive(true);
        }
    }
}