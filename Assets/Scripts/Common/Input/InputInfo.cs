
public struct InputInfo
{
    public bool IsLeftButtonDown { get; set; }

    public float xPos;

    public float yPos;

    public bool IsKeyPressed { get; set; }

    public bool IsTabPressed { get; set; }
    public bool Is1Pressed { get; set; }
    public bool Is2Pressed { get; set; }
    public bool Is3Pressed { get; set; }
    public bool IsAPressed { get; set; }
    public bool IsWPressed { get; set; }
    public bool IsEPressed { get; set; }
    public bool IsDPressed { get; set; }
    public bool IsSPressed { get; set; }
    public bool IsShiftPressed { get; set; }
    public bool IsScrollUp { get; set; }
    public int GetNumberKeyPressed()
    {
        if (Is1Pressed)
        {
            return 0;
        }
        else if (Is2Pressed)
        {
            return 1;
        }
        else if (Is3Pressed)
        {
            return 2;
        }

        return -1;
    }

}
