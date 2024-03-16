using System;
using System.Collections.Generic;
using AIModule;
using UnityEngine;

namespace Commands
{
    public sealed class StateCommands<T>
    {
        private StateMachine stateMachine;
        public T currentKey;
        private IAIState currentState;

        private Stack<KeyValuePair<T, IAIState>> states = new Stack<KeyValuePair<T, IAIState>>();

        public StateCommands(StateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }
        
        public void Execute(T key, IAIState state)
        {
            if(currentState != null)
                states.Push(new KeyValuePair<T, IAIState>(currentKey, currentState));
            
            SwitchState(key, state);
        }
        
        public void Undo()
        {
            if(states.Count == 0)
                return;
            
            var state = states.Pop();
            SwitchState(state.Key, state.Value);
        }
        
        private void SwitchState(T key, IAIState state)
        {
            currentKey = key;
            stateMachine.SwitchState(state);
            currentState = state;
            Debug.Log(currentKey);
        }
    }
}