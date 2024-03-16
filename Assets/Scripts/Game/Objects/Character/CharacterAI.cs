using System;
using AIModule;
using Atomic.Objects;
using Game.Common;
using UnityEngine;
using Game.Components;

namespace Game.Objects
{
    [Serializable]
    public class CharacterAI
    {
        public bool enable;
        
        [Section]
        public TriggerComponent triggerComponent;
        
        [Get(ObjectAPI.Blackboard), SerializeField]
        private Blackboard blackboard;

        [SerializeField]
        private AIBehaviour behaviour;

        [Get(ObjectAPI.StateMachine)]
        private StateMachine stateMachine = new();
        
        public void Compose(AtomicObject character)
        {
            if (enable)
            {
                triggerComponent.Compose();
                blackboard.SetObject(BlackboardAPI.Character, character);
                character.AddProperty(nameof(Blackboard), blackboard);
                stateMachine.Compose(blackboard);
            }
        }

        public void OnEnable()
        {
            if (enable)
                behaviour.OnStart();
        }

        public void OnDisable()
        {
            if (enable)
                behaviour.OnStop();
        }

        public void FixedUpdate(float fixedDeltaTime)
        {
            if (enable)
            {
                stateMachine.FixedUpdate(fixedDeltaTime);
                behaviour.OnUpdate(fixedDeltaTime);
                triggerComponent.FixedUpdate(fixedDeltaTime);
            }
        }

        public void OnDrawGizmos()
        {
            if (enable)
            {
                behaviour.OnGizmos();
                triggerComponent.OnDrawGizmos();
            }
        }
    }
}