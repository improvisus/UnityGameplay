using System;
using System.Collections.Generic;
using AIModule;

namespace Commands
{
    public sealed class StateCommands<T>
    {
        private StateMachine<T> stateMachine;
        public T currentKey => stateMachine.CurrentKey;
        private IAIState currentState;

        private Stack<KeyValuePair<T, IAIState>> states = new Stack<KeyValuePair<T, IAIState>>();

        public void Compose(StateMachine<T> stateMachine)
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
            stateMachine.SwitchState(key, state);
            currentState = state;
        }
    }
}