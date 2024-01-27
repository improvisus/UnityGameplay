using Atomic.Elements;
using UnityEngine;

namespace Game.AnimationMechanics
{
    public class FireAnimMechanics
    {
        private static readonly int Shoot = Animator.StringToHash("Shoot");

        private readonly Animator animator;
        private readonly IAtomicObservable fireEvent;

        public FireAnimMechanics(Animator animator, IAtomicObservable fireEvent)
        {
            this.animator = animator;
            this.fireEvent = fireEvent;
        }
        
        public void OnEnable()
        {
            fireEvent.Subscribe(OnFire);
        }

        public void OnDisable()
        {
            fireEvent.Unsubscribe(OnFire);
        }
        
        private void OnFire()
        {
            animator.SetTrigger(Shoot);
        }
    }
}