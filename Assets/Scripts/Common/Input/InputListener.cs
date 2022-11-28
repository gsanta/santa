public abstract class InputListener
{
    private InputHandler _handler;

    public bool IsListenerDisabled { get; set; }

    public virtual void OnKeyPressed(InputInfo inputInfo) { }

    public virtual void OnClick(InputInfo inputInfo) { }

    public virtual void OnMouseMove(InputInfo inputInfo) { }

    public virtual void OnScroll(InputInfo inputInfo) { }

    public void Register(InputHandler handler)
    {
        handler.AddListener(this);
        _handler = handler;
    }

    public void UnRegister()
    {
        if (_handler)
        {
            _handler.RemoveListener(this);
        }
    }
}
