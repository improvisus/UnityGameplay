using System;
using Atomic.Behaviours;
using Atomic.Objects;
using UnityEngine;

namespace Game.Objects
{
    public class Character : AtomicBehaviour
    {
        [Section]
        [SerializeField]
        private CharacterCore core;
        [Section]
        [SerializeField]
        private CharacterView view;
        [Section]
        [SerializeField]
        private CharacterAI ai;
        
        private void Awake()
        {
            Compose();
        }

        public override void Compose()
        {
            base.Compose();
            
            core.Compose(this);
            view.Compose(core);
            ai.Compose(this);
        }

        private void OnEnable()
        {
            base.Enable();
            
            core.OnEnable();
            view.OnEnable();
            ai.OnEnable();
        }

        private void OnDisable()
        {
            base.Disable();
            
            core.OnDisable();
            view.OnDisable();
            ai.OnDisable();
        }
        
        private void Update()
        {
            var deltaTime = Time.deltaTime;
            
            base.OnUpdate(deltaTime);
            
            core.Update(deltaTime);
            view.Update(deltaTime);
        }
        
        public void LateUpdate()
        {
            var deltaTime = Time.deltaTime;
            
            base.OnLateUpdate(deltaTime);
        }
        
        private void FixedUpdate()
        {
            var fixedDeltaTime = Time.fixedDeltaTime;
            
            base.OnFixedUpdate(fixedDeltaTime);
            
            ai.FixedUpdate(fixedDeltaTime);
        }
        private void OnDrawGizmos()
        {
            ai.OnDrawGizmos();
        }
        private void OnDestroy()
        {
            core.Dispose();
        }
    }
}
