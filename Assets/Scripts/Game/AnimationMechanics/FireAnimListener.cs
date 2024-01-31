using Atomic.Elements;
using Game.Common;
namespace Game.AnimationMechanics
{
    public class FireAnimListener
    {
        private const string FireEvent = "shoot";
        private readonly AnimatorDispatcher animatorDispatcher;
        private readonly IAtomicAction fireAction;
        
        public FireAnimListener(AnimatorDispatcher animatorDispatcher, IAtomicAction fireAction)
        {
            this.animatorDispatcher = animatorDispatcher;
            this.fireAction = fireAction;
        }

        public void OnEnable()
        {
            animatorDispatcher.OnEventReceived += OnEventReceived;
        }
        
        public void OnDisable()
        {
            animatorDispatcher.OnEventReceived -= OnEventReceived;
        }
        
        private void OnEventReceived(string eventКey)
        {
            if (eventКey == FireEvent)
                fireAction?.Invoke();
        }
    }
}