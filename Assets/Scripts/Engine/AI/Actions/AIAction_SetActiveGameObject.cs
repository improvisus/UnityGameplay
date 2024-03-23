using AIModule;
using UnityEngine;

namespace Game.Engine.AI.AIActions
{
    [CreateAssetMenu(
        fileName = "AIAction_SetActiveGameObject",
        menuName = "Engine/AI/New AIAction_SetActiveGameObject"
    )]
    public class AIAction_SetActiveGameObject : AIAction
    {
        [BlackboardKey]
        [SerializeField]
        private ushort gameObjectKey;

        [SerializeField]
        private bool active;

        public override void Perform(IBlackboard blackboard)
        {
            if (!blackboard.TryGetObject(gameObjectKey, out GameObject obj))
                return;

            obj.SetActive(active);
        }
    }
}