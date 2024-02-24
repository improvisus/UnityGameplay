using Atomic.Elements;
using Game.Common;
namespace Game.AnimationMechanics
{
    public class FireAnimListener
    {
        private readonly AnimatorDispatcher animatorDispatcher;
        private readonly IAtomicAction fireAction;
        
        public FireAnimListener(AnimatorDispatcher animatorDispatcher, IAtomicAction fireAction)
        {
            this.animatorDispatcher = animatorDispatcher;
            this.fireAction = fireAction;
        }

        public void OnEnable()
        {
            animatorDispatcher.AddListener(AnimEventKey.OnShoot, OnShootEvent);
        }


        public void OnDisable()
        {
            animatorDispatcher.RemoveListener(AnimEventKey.OnShoot, OnShootEvent);
        }
        
        private void OnShootEvent()
        {
            fireAction?.Invoke();
        }
    }
}