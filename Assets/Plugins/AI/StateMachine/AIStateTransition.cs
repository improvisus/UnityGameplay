using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AIModule
{
    //TODO: написать Editor для HFSM
    [Serializable]
    internal sealed class AIStateTransition
    {
        [HorizontalGroup]
        [ValueDropdown(nameof(DrawStateNames))]
        [LabelText("From")]
        [SerializeField]
        internal int sourceState;

        [HorizontalGroup]
        [ValueDropdown(nameof(DrawStateNames))]
        [LabelText("To")]
        [SerializeField]
        internal int targetState;

        [Space]
        [SerializeField]
        private AICondition condition;

        [SerializeField]
        private AIAction[] actions;

        internal bool Check(IBlackboard blackboard)
        {
            return this.condition.Check(blackboard);
        }

        internal void Perform(IBlackboard blackboard)
        {
            for (int i = 0, count = this.actions.Length; i < count; i++)
            {
                AIAction action = this.actions[i];
                action.Perform(blackboard);
            }
        }

#if UNITY_EDITOR
        
        internal Func<ValueDropdownList<int>> _drawCallback;
        
        internal ValueDropdownList<int> DrawStateNames()
        {
            return _drawCallback?.Invoke();
        }
#endif
        
        
    }
}