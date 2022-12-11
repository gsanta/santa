
public class RoamStateTransition : StateTransition
{
    private Character _character;

    private float _idleTimer = 5.0f; 

    public RoamStateTransition(Character character)
    {
        _character = character;
    }

    public override string Check()
    {
        if (_character.IsCurrentState(IdleState.Name) && _character.GetCurrentState().GetElapsedTime() > _idleTimer)
        {
            return RoamState.Name;
        }

        return null;
    }
}
