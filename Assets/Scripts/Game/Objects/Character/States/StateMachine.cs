using System;
using AIModule;

[Serializable]
public class StateMachine
{
    private IBlackboard blackboard;
    
    private IAIState currentState;
    
    public void Compose(IBlackboard blackboard)
    {
        this.blackboard = blackboard;
    }
    
    public void SwitchState(IAIState state)
    {
        currentState?.OnStop(blackboard);
        currentState = state;
        currentState?.OnStart(blackboard);
    }

    public void FixedUpdate(float fixedDeltaTime)
    {
        currentState?.OnUpdate(blackboard, fixedDeltaTime);
    }
}