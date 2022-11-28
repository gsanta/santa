
using UnityEngine;

public class Services : MonoBehaviour
{
    private static Services _instance;

    private PathGrid _pathGrid;

    private void Awake()
    {
        _instance = this;
    }

    public static Services GetInstance()
    {
        return _instance;
    }

    public void SetPathGrid(PathGrid pathGrid)
    {
        if (_pathGrid)
        {
            throw new System.Exception("PathGrid service already registered");
        }

        _pathGrid = pathGrid;
    }

    public PathGrid GetPathGrid()
    {
        return _pathGrid;
    }
}
