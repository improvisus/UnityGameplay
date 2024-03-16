using AIModule;
using Atomic.Objects;
using Commands;
using Game.Objects.States;
using UnityEngine;
using UnityEngine.Serialization;
namespace Game.AI.Mechanics
{
    [CreateAssetMenu(
        fileName = "AIExecuteAttackMechanics",
        menuName = "Content/AI/Mechanics/New AIExecuteAttackMechanics"
    )]
    public class AIExecuteAttackMechanics : AIMechanics
    {
        [FormerlySerializedAs("runAttackEnabled")]
        [SerializeField, BlackboardKey]
        private ushort executeAttackEnabled;
        
        [SerializeField, BlackboardKey]
        private ushort stateCommands;

        [SerializeField, BlackboardKey]
        private ushort target;

        public override void OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if (!blackboard.TryGetBool(this.executeAttackEnabled, out bool executeAttackEnabled) ||
                !blackboard.TryGetObject(this.stateCommands, out StateCommands<StateCommand> stateCommands) ||
                !blackboard.TryGetObject(this.target, out IAtomicObject target))
            {
                return;
            }

            if(!executeAttackEnabled)
                return;
            
            if (stateCommands.currentKey == StateCommand.Attack && target == null)
                stateCommands.Undo();

            if(stateCommands.currentKey != StateCommand.Attack && target != null)
                stateCommands.Execute(StateCommand.Attack, new AttackState(target));
        }
    }
}