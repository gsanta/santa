

public abstract class GameEntityState
{
    private string _stateName;

    private bool _isActive;

    protected GameEntityState(string stateName)
    {
        _stateName = stateName;
    }

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
