using System;
using Game.AnimationMechanics;
using Game.Common;
using UnityEngine;

namespace Game.Objects
{
    [Serializable]
    public class CharacterView
    {
        [SerializeField]
        private Animator animator;
        [SerializeField]
        private AnimatorDispatcher animatorDispatcher;
        
        private MoveAnimMechanics moveAnimMechanics;
        private FireAnimMechanics fireAnimMechanics;
        private FireAnimListener fireAnimListener;
        
        public void Compose(CharacterCore core)
        {
            moveAnimMechanics = new MoveAnimMechanics(animator, core.moveComponent.IsMoving);
            
            fireAnimMechanics = new FireAnimMechanics(animator, core.attackComponent.AttackRequest, core.attackComponent.AttackCondition);
            fireAnimListener = new FireAnimListener(animatorDispatcher, core.attackComponent.AttackAction);
        }

        public void OnEnable()
        {
            fireAnimMechanics.OnEnable();
            fireAnimListener.OnEnable();
        }
        
        public void OnDisable()
        {
            fireAnimMechanics.OnDisable();
            fireAnimListener.OnDisable();
        }
        
        public void Update(float deltaTime)
        {
            moveAnimMechanics.Update();
        }
    }
}