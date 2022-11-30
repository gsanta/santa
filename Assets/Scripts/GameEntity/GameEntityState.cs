

using UnityEngine;

public abstract class GameEntityState : MonoBehaviour
{
    protected string _stateName;

    [SerializeField] private bool _isActive = false;

    public string GetName()
    {
        return _stateName;
    }

    public void SetIsActive(bool isActive)
    {
        _isActive = isActive;

        if (_isActive)
        {
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

    protected virtual void OnActivated() { }

    protected virtual void OnDeactivated() { }
}
