using AIModule;
using Atomic.Elements;
using Atomic.Objects;
using Game.Common;
using UnityEngine;
namespace Game.AI.Mechanics
{
    [CreateAssetMenu(
        fileName = "AIAttackMechanics",
        menuName = "Content/AI/Mechanics/New AIAttackMechanics"
    )]
    public class AIAttackMechanics : AIMechanics
    {
        [SerializeField, BlackboardKey]
        private ushort character;
        
        public override void OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if (!blackboard.TryGetObject(this.character, out IAtomicObject character))
                return;
            
            var fireRequest = character.Get<IAtomicAction>(ObjectAPI.AttackRequest);
            fireRequest?.Invoke();
        }
    }
}