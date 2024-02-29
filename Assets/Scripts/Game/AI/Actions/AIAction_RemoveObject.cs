using AIModule;
using UnityEngine;

namespace Game.AI.Actions
{
    [CreateAssetMenu(
        fileName = "AIAction_RemoveObject",
        menuName = "Content/AI/Action/New AIAction_RemoveObject"
    )]
    public class AIAction_RemoveObject : AIAction
    {
        [SerializeField, BlackboardKey]
        private ushort key;

        public override void Perform(IBlackboard blackboard)
        {
            blackboard.DeleteObject(key);
        }
    }
}