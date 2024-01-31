using System;
using Atomic.Objects;
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
        [SerializeField, Get(ObjectAPI.AnimatorDispatcher)]
        private AnimatorDispatcher animatorDispatcher;
        
        [SerializeField]
        private AudioSource audioSource;
        [SerializeField]
        private AudioClip fireSFX;
        
        [SerializeField]
        private ParticleSystem fireVFX;
        
        private MoveAnimMechanics moveAnimMechanics;
        private FireAnimMechanics fireAnimMechanics;
        private FireAnimListener fireAnimListener;
        
        public void Compose(CharacterCore core)
        {
            moveAnimMechanics = new MoveAnimMechanics(animator, core.moveComponent.IsMoving);
            fireAnimMechanics = new FireAnimMechanics(animator, core.fireComponent.FireRequest);
            fireAnimListener = new FireAnimListener(animatorDispatcher, core.fireComponent.FireAction);
            
            core.fireComponent.FireEvent.Subscribe(() => fireVFX.Play(withChildren: true));
            core.fireComponent.FireEvent.Subscribe(() => audioSource.PlayOneShot(fireSFX));
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