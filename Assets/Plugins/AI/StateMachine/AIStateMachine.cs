using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AIModule
{
    //TODO: написать Editor для HFSM
    [Serializable]
    public sealed class AIStateMachine : AIState, ISerializationCallbackReceiver, IAIValidate
    {
        [Header("States")]
        [ValueDropdown(nameof(DrawStateNames))]
        [SerializeField]
        private int currentState;

#if UNITY_EDITOR
        [ListDrawerSettings(OnBeginListElementGUI = nameof(DrawStateLabel))]
#endif
        [SerializeReference]
        private IAIState[] states = Array.Empty<IAIState>();

        [Header("Transitions")]
        [SerializeField]
        private AIStateTransition[] transitions = Array.Empty<AIStateTransition>();

        [ShowInInspector, ReadOnly, HideInEditorMode]
        private Dictionary<int, List<AIStateTransition>> transitionMap;

        public override void OnStart(IBlackboard blackboard)
        {
            base.OnStart(blackboard);

            IAIState currentState = this.states[this.currentState];
            currentState.OnStart(blackboard);
        }

        public override void OnStop(IBlackboard blackboard)
        {
            base.OnStop(blackboard);

            IAIState currentState = this.states[this.currentState];
            currentState.OnStop(blackboard);
        }

        public override void OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            base.OnUpdate(blackboard, deltaTime);
            
            //Switch to next state:
            if (this.transitionMap.TryGetValue(this.currentState, out List<AIStateTransition> transitions))
            {
                for (int i = 0, count = transitions.Count; i < count; i++)
                {
                    AIStateTransition transition = transitions[i];
                    if (transition.Check(blackboard))
                    {
                        IAIState previousState = this.states[this.currentState];
                        previousState.OnStop(blackboard);
            
                        transition.Perform(blackboard);
            
                        this.currentState = transition.targetState;
            
                        IAIState nextState = this.states[this.currentState];
                        nextState.OnStart(blackboard);
                        break;
                    }
                }
            }

//            Debug.LogError(this.currentState);
            //Update current state:
            IAIState currentState = this.states[this.currentState];
            currentState.OnUpdate(blackboard, deltaTime);
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            //Init transitions:
            this.transitionMap = new Dictionary<int, List<AIStateTransition>>();
            foreach (var transition in this.transitions)
            {
                int sourceState = transition.sourceState;
                if (!this.transitionMap.TryGetValue(sourceState, out List<AIStateTransition> transitions))
                {
                    transitions = new List<AIStateTransition>();
                    this.transitionMap.Add(sourceState, transitions);
                }

                transitions.Add(transition);
            }
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
        }

#if UNITY_EDITOR
        private void DrawStateLabel(int index)
        {
            if (this.states == null)
            {
                return;
            }

            IAIState state = this.states[index];

            string label = string.IsNullOrWhiteSpace(state.ToString())
                ? $"{index + 1}. Undefined state"
                : $"{index + 1}. {state}";

            GUILayout.Space(4);
            GUILayout.Label(label);
        }

        private ValueDropdownList<int> DrawStateNames()
        {
            var result = new ValueDropdownList<int>();

            if (this.states == null)
            {
                return result;
            }

            for (int i = 0, count = this.states.Length; i < count; i++)
            {
                var state = this.states[i];
                if (state != null)
                {
                    result.Add(new ValueDropdownItem<int>(state.ToString(), i));
                }
            }

            return result;
        }
        
        
        public void OnValidate()
        {
            foreach (var transition in this.transitions)
            {
                transition._drawCallback = this.DrawStateNames;
            }

            foreach (var state in this.states)
            {
                if (state is IAIValidate validate)
                {
                    validate.OnValidate();
                }
            }
        }
#endif
    }
}