using AIModule;
using UnityEngine;
namespace Game.Engine.AI.Conditions
{
    [CreateAssetMenu(
        fileName = "AICondition_KeyExists",
        menuName = "Engine/AI/New AICondition_KeyExists"
    )]
    public class AICondition_KeyExists : AICondition
    {
        [SerializeField, BlackboardKey]
        private ushort key;
        
        public override bool Check(IBlackboard blackboard)
        {
            return blackboard.HasKey(key);
        }
    }
}