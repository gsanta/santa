using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterState : MonoBehaviour
{
    protected string _stateName;

    [SerializeField] private bool _isActive = false;

    protected bool _isEnded = false;

    private float _elapsedTime = 0;

    private Character _character;

    private List<StateTransition> _stateTransitions = new();

    private void Start()
    {
        _character = GetComponent<Character>();

        OnStarted();
    }

    private void Update()
    {
        if (!IsActive())
        {
            return;
        }

        foreach (StateTransition transition in _stateTransitions)
        {
            var newStateName = transition.Check();
            
            if (newStateName != null)
            {
                _character.SetCurrentState(newStateName);
                return;
            }
        }

        _elapsedTime += Time.deltaTime;
        OnUpdated();
    }

    protected void AddStateTransition(StateTransition transition)
    {
        _stateTransitions.Add(transition);
    }

    public string GetName()
    {
        return _stateName;
    }

    public float GetElapsedTime()
    {
        return _elapsedTime;
    }

    public void SetIsActive(bool isActive)
    {
        _isActive = isActive;
        
        if (_isActive)
        {
            _isEnded = false;
            _elapsedTime = 0;
            OnActivated();
        } else
        {
            OnDeactivated();
        }
    }

    public bool IsActive()
    {
        return _isActive;
    }

    public bool IsEnded()
    {
        return _isEnded;
    }

    protected virtual void OnActivated() { }    

    protected virtual void OnDeactivated() { }

    protected virtual void OnUpdated() { }

    protected virtual void OnStarted() { }
}
