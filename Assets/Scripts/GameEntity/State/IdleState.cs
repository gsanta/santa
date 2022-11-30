using System;

public class IdleState : GameEntityState
{
    public static string Name = "Idle";

    private Character _character;

    private void Start()
    {
        _character = GetComponent<Character>();
    }

    protected override void OnActivated()
    {
        Invoke("Finish", 5.0f);
    }

    protected override void OnDeactivated()
    {

    }

    private void Finish()
    {
        var nextState = Array.Find(_character.States, (state) => state.GetName() == RoamState.Name);

        SetIsActive(false);
        nextState.SetIsActive(true);
    }
}
