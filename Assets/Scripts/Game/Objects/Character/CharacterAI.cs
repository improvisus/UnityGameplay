using System;
using AIModule;
using Atomic.Elements;
using Atomic.Objects;
using Commands;
using Game.Common;
using Sirenix.OdinInspector;
using UnityEngine;
using Game.AI.Sensors;

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

        [Get(ObjectAPI.StateMachine), SerializeField ,ReadOnly]
        private StateMachine<StateCommand> stateMachine;
        
        [Get(ObjectAPI.StateCommands)]
        private StateCommands<StateCommand> stateCommands = new();
        
        [SerializeField]
        private AtomicVariable<bool> enabledTriggerMechanics;
        [SerializeField]
        private Transform center;
        [SerializeField]
        private AtomicVariable<float> radius;
        [SerializeField]
        private AtomicVariable<LayerMask> layerMask;

        [Get(ObjectAPI.FoundTarget)]
        private AtomicVariable<IAtomicObject> foundTarget = new();
        private TriggerSensorMechanics triggerSensorMechanics;
        
        public void Compose(AtomicObject character)
        {
            if (enable)
            {
                blackboard.SetObject(BlackboardAPI.Character, character);
                character.AddProperty(nameof(Blackboard), blackboard);
                stateMachine.Compose(blackboard);
                stateCommands.Compose(stateMachine);

                triggerSensorMechanics = new TriggerSensorMechanics(enabledTriggerMechanics, center, radius, layerMask, foundTarget);
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
                triggerSensorMechanics.FixedUpdate(fixedDeltaTime);
            }
        }

        public void OnDrawGizmos()
        {
            if (enable)
                behaviour.OnGizmos();
        }
    }
}

public enum StateCommand
{
    Idle,
    MoveToPoint,
    MoveToTarget,
    Patrol,
    Attack,
}

[Serializable]
public class StateMachine<T>
{
    private IBlackboard blackboard;
    
    public T CurrentKey;
    public IAIState CurrentState => currentState;
    private IAIState currentState;

    public void Compose(IBlackboard blackboard)
    {
        this.blackboard = blackboard;
    }
    
    public void SwitchState(T key, IAIState state)
    {
        currentState?.OnStop(blackboard);
        CurrentKey = key;
        currentState = state;
        currentState?.OnStart(blackboard);
    }

    public void FixedUpdate(float fixedDeltaTime)
    {
        currentState?.OnUpdate(blackboard, fixedDeltaTime);
    }
}