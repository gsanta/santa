public class PathNode
{
    public readonly int X;

    public readonly int Y;

    public int GCost;

    public int HCost;

    public int FCost;

    public bool IsWalkable = true;
    public PathNode CameFromNode;

    public PathNode(int x, int y)
    {
        X = x;
        Y = y;
    }

    public void CalculateFCost()
    {
        FCost = GCost + HCost;
    }

    public override string ToString()
    {
        return X + ", " + Y;
    }
}