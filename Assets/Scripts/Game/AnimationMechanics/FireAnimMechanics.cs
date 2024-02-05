using Atomic.Elements;
using UnityEngine;

namespace Game.AnimationMechanics
{
    public class FireAnimMechanics
    {
        private static readonly int Shoot = Animator.StringToHash("Shoot");

        private readonly Animator animator;
        private readonly IAtomicObservable fireEvent;
        private readonly AtomicFunction<bool> attackCondition;

        public FireAnimMechanics(Animator animator, IAtomicObservable fireEvent, AtomicFunction<bool> attackCondition)
        {
            this.animator = animator;
            this.fireEvent = fireEvent;
            this.attackCondition = attackCondition;
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
            if(attackCondition.Invoke())
                animator.SetTrigger(Shoot);
        }
    }
}