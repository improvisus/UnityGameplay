using Atomic.Behaviours;
using Atomic.Objects;
using UnityEngine;

namespace Game.Objects
{
    public class Character : AtomicBehaviour
    {
        [SerializeField, Section]
        private CharacterCore core;
        [SerializeField, Section]
        private CharacterView view;
        
        private void Awake()
        {
            Compose();
        }

        public override void Compose()
        {
            base.Compose();
            core.Compose(this);
            view.Compose(core);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            core.OnEnable();
            view.OnEnable();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            core.OnDisable();
            view.OnDisable();
        }
        
        protected override void Update()
        {
            base.Update();
            core.Update(Time.deltaTime);
            view.Update(Time.deltaTime);
        }
        
        private void OnDestroy()
        {
            core.Dispose();
        }
    }
}
