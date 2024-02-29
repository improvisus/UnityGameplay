using AIModule;
using UnityEngine;

namespace Game.AI.Mechanics
{
    [CreateAssetMenu(
        fileName = "AIWaitForSecondsMechanics",
        menuName = "Content/AI/Mechanics/New AIWaitForSecondsMechanics"
    )]
    public class AIWaitForSecondsMechanics : AIMechanics
    {
        [SerializeField, BlackboardKey]
        private ushort waitForSecondsEnabled;

        [SerializeField, BlackboardKey]
        private ushort startTime;
        
        [SerializeField]
        private float duration = 1;

        public override void OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if (!blackboard.TryGetBool(this.waitForSecondsEnabled, out bool waitForSecondsEnabled) ||
                !blackboard.TryGetFloat(this.startTime, out float startTime))
                return;
            
            if (!waitForSecondsEnabled)
                return;

            if (Time.time - startTime >= duration)
                blackboard.SetBool(BlackboardAPI.WaitForSecondsEnabled, false);
        }
    }
}