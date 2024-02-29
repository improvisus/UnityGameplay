using System;
using AIModule;
using Atomic.Objects;
using Game.Common;
using UnityEngine;

namespace Game.Objects
{
    [Serializable]
    public class CharacterAI
    {
        public bool enable;
        [Get(ObjectAPI.Blackboard), SerializeField]
        private Blackboard blackboard;

        [SerializeField]
        private AIBehaviour behaviour;

        public void Compose(AtomicObject character)
        {
            if (enable)
            {
                blackboard.SetObject(BlackboardAPI.Character, character);
                character.AddProperty(nameof(Blackboard), blackboard);
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

        public void FixedUpdate(float deltaTime)
        {
            if (enable)
                behaviour.OnUpdate(deltaTime);
        }

        public void OnDrawGizmos()
        {
            if (enable)
                behaviour.OnGizmos();
        }
    }
}