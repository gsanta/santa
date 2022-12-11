using UnityEngine;

public class FightState : CharacterState
{
    public static string Name = "Fight";

    private Character _character;

    private void Awake()
    {
        _stateName = Name;
        _character = GetComponent<Character>();
    }

    protected override void OnActivated()
    {
        InvokeRepeating("HitTheTarget", 0f, 5f);
    }

    private void HitTheTarget()
    {
        _character.Movement.SetJabbing();
    }

    protected override void OnDeactivated()
    {

    }
}
