
public class IdleStateTransition : StateTransition
{
    private Character _character;

    public IdleStateTransition(Character character)
    {
        _character = character;
    }

    public override string Check()
    {
        if (_character.IsCurrentState(RoamState.Name) && _character.GetCurrentState().IsEnded())
        {
            return IdleState.Name; 
        }
        return null;
    }
}
