

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
    }

    public bool IsActive()
    {
        return _isActive;
    }
}
