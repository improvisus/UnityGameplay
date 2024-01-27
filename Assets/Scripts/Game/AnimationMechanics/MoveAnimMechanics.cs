using Atomic.Elements;
using UnityEngine;

namespace Game.AnimationMechanics
{
    public class MoveAnimMechanics
    {
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");

        private readonly Animator animator;
        private readonly IAtomicValue<bool> isMoving;

        public MoveAnimMechanics(Animator animator, IAtomicValue<bool> isMoving)
        {
            this.animator = animator;
            this.isMoving = isMoving;
        }

        public void Update()
        {
            animator.SetBool(IsMoving, isMoving.Value);
        }
    }
}