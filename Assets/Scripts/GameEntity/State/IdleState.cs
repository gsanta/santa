using System;
using UnityEngine;

public class IdleState : CharacterState
{
    public static string Name = "Idle";

    private Character _character;

    private void Awake()
    {
        _stateName = Name;
        _character = GetComponent<Character>();
    
        AddStateTransition(new RoamStateTransition(_character));
        AddStateTransition(new StartChaseTransition(_character));
    }

    private void Finish()
    {
        var player = CharacterStore.GetInstance().GetPlayer();

        CharacterState nextState = null;

        if (Vector2.Distance(player.GetPosition(), _character.GetPosition()) < 2.0f)
        {
            nextState = Array.Find(_character.States, (state) => state.GetName() == FightState.Name);
        } else
        {
            nextState = Array.Find(_character.States, (state) => state.GetName() == RoamState.Name);
        }


        SetIsActive(false);
        nextState.SetIsActive(true);
    }
}
