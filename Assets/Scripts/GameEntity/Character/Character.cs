using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterState[] States { get; private set; }

    public Movement Movement { get; private set; }

    [SerializeField] private bool _isPlayer;

    public CharacterState _currentState;

    public Rigidbody2D Rigidbody2D;

    private void Awake()
    {
        States = GetComponents<CharacterState>();
        Movement = GetComponent<Movement>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        CharacterStore.GetInstance().AddCharacter(this);

        foreach (CharacterState state in States) 
        {
            if (state.IsActive())
            {
                _currentState = state;
                state.SetIsActive(true);
                return;
            }
        }
    }

    public bool IsPlayer() {
        return _isPlayer;
    }

    public bool IsCurrentState(string name)
    {
        if (_currentState == null)
        {
            return name == null;
        }
        return _currentState.GetName() == name;
    }

    public CharacterState GetCurrentState()
    {
        return _currentState;
    }

    public void SetCurrentState(string name)
    {
        if (_currentState != null)
        {
            _currentState.SetIsActive(false);
        }

        if (name == null)
        {
            _currentState = null;
        } else
        {
            GetState(name).SetIsActive(true);
        }
    }

    public Vector2 GetPosition()
    {
        return transform.position;
    }

    public CharacterState GetState(string name)
    {
        return Array.Find(States, (state) => state.GetName() == name);
    }
}
