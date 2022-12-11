

using UnityEngine;

public class StartChaseTransition : StateTransition
{
    private Character _character;

    public StartChaseTransition(Character character)
    {
        _character = character;
    }

    public override string Check()
    {
        var position = _character.GetPosition();
        var playerPosition = CharacterStore.GetInstance().GetPlayer().GetPosition();
    
        if (Vector2.Distance(position, playerPosition) < 5.0f && !_character.IsCurrentState(ChaseState.Name))
        {
            return ChaseState.Name;
        }
        return null;
    }
}
