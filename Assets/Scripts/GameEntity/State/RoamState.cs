
public class RoamState : GameEntityState
{
    public static string Name = "Roam";

    private PathFinding _pathFinding = new PathFinding();

    private PathGrid _pathGrid;

    public RoamState(PathGrid pathGrid) : base(RoamState.Name)
    {
        _pathGrid = pathGrid;
    }
}
