using AIModule;
using Game.Extensions;
using UnityEngine;

namespace Game.AI.Conditions
{
    [CreateAssetMenu(
        fileName = "AICondition_HasEnable",
        menuName = "Content/AI/Condition/New AICondition_HasEnable"
    )]
    public class AICondition_HasEnable : AICondition
    {
        [SerializeField, BlackboardKey]
        private ushort enable;

        [SerializeField]
        private bool disable;
        
        public override bool Check(IBlackboard blackboard)
        {
            bool hasEnable = blackboard.TryGetBool(this.enable, out bool enable) && enable.IsNotNull();

            if(enable != null)
                hasEnable = enable;
            
            if (disable)
                hasEnable = !hasEnable;
            
            return hasEnable;
        }
    }
}