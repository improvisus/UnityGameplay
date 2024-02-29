using AIModule;
using UnityEngine;
namespace Game.AI.Actions
{
    [CreateAssetMenu(
        fileName = "AIAction_EnableMechanics",
        menuName = "Content/AI/Action/New AIAction_EnableMechanics"
    )]
    public class AIAction_EnableMechanics : AIAction
    {
        [SerializeField, BlackboardKey]
        private ushort enable;
        
        [SerializeField]
        private bool disable;
        
        public override void Perform(IBlackboard blackboard)
        {
            blackboard.SetBool(enable, !disable);
        }
    }
}