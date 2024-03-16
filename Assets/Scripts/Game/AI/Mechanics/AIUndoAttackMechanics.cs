using AIModule;
using Atomic.Objects;
using Commands;
using Game.Objects.States;
using UnityEngine;
using UnityEngine.Serialization;
namespace Game.AI.Mechanics
{
    [CreateAssetMenu(
        fileName = "AIUndoAttackMechanics",
        menuName = "Content/AI/Mechanics/New AIUndoAttackMechanics"
    )]
    public class AIUndoAttackMechanics : AIMechanics
    {
        [FormerlySerializedAs("runAttackEnabled")]
        [SerializeField, BlackboardKey]
        private ushort undoAttackEnabled;
        
        [SerializeField, BlackboardKey]
        private ushort stateCommands;

        [SerializeField, BlackboardKey]
        private ushort target;

        public override void OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if (!blackboard.TryGetBool(this.undoAttackEnabled, out bool undoAttackEnabled) ||
                !blackboard.TryGetObject(this.stateCommands, out StateCommands<StateCommand> stateCommands) ||
                !blackboard.TryGetObject(this.target, out IAtomicObject target))
            {
                return;
            }

            if(!undoAttackEnabled)
                return;
            
            if (target == null)
                stateCommands.Undo();
        }
    }
}