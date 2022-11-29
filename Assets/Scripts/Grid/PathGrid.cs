

using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathGrid : MonoBehaviour
{
    [SerializeField] private Tilemap backgroundTileMap;

    [SerializeField] private Tilemap objectsTileMap;

    private readonly float _cellSize = 1.0f;

    private PathNode[] _gridArray;

    private Vector2Int _gridSize;

    private Vector2 _worldSize;

    private void Awake()
    {
        Services.GetInstance().SetPathGrid(this);

        var bounds = backgroundTileMap.GetComponent<Renderer>().bounds;

        var topLeft = new Vector2(bounds.min.x, bounds.max.y);
        var bottomRight = new Vector2(bounds.max.x, bounds.min.y);

        var worldWidth = Mathf.Abs(topLeft.x - bottomRight.x);
        var worldHeight = Mathf.Abs(topLeft.y - bottomRight.y);

        _gridSize = new Vector2Int((int)(worldWidth / _cellSize), (int)(worldHeight / _cellSize));
        _worldSize = new Vector2(worldWidth, worldHeight);

        CreateNodes();
        SetWalkableNodes();
    }

    public Vector2Int GetGridCenter()
    {
        var offsetX = Mathf.CeilToInt(_gridSize.x / 2.0f);
        var offsetY = Mathf.CeilToInt(_gridSize.y / 2.0f);
        return new Vector2Int(offsetX, offsetY);
    }

    public Vector2Int GetSize()
    {
        return _gridSize;
    }

    public float GetCellSize()
    {
        return _cellSize;
    }

    public PathNode[] GetAllNodes()
    {
        return _gridArray;
    }


    public PathNode GetNode(int x, int y)
    {
        var index = ToArrayIndex(x, y);
        if (index < 0 || index > _gridArray.Length - 1)
        {
            return null;
        }
        return _gridArray[index];
    }

    public PathNode GetNodeAtWorldPos(Vector2 pos)
    {
        var gridPos = GetGridPosition(pos);
        if (gridPos.HasValue)
        {
            return GetNode(gridPos.Value.x, gridPos.Value.y);
        }
        return null;
    }

    public Vector2? GetNodeWorldPosition(PathNode node)
    {
        GetNodePosition(node, out int x, out int y);

        if (x != -1)
        {
            return GetWorldPosition(x, y);
        }

        return null;
    }

    public Vector3 GetWorldPosition(int x, int y, float worldZ)
    {
        var worldPos = GetWorldPosition(x, y);
        return new Vector3(worldPos.x, worldPos.y, worldZ);
    }

    public Vector2 GetWorldPosition(int x, int y)
    {
        float worldX = x * _cellSize - _worldSize.x / 2;
        float worldY = y * _cellSize - _worldSize.y / 2;

        if (_gridSize.x % 2 == 0) {
            worldX += _cellSize / 2;
        }

        if (_gridSize.y % 2 == 0)
        {
            worldY += _cellSize / 2;
        }

        return new Vector2(worldX, worldY);

    }

    public Vector2 GetRandomWorldPosition()
    {
        var x = UnityEngine.Random.Range(0, _gridSize.x);
        var y = UnityEngine.Random.Range(0, _gridSize.y);

        return GetWorldPosition(x, y);
    }

    public Vector2Int? GetGridPosition(Vector2 worldPosition)
    {
        var vec = (worldPosition + _worldSize / 2) / _cellSize;
        var x = (int)Mathf.Floor(vec.x);
        var y = (int)Mathf.Floor(vec.y);

        return !IsWithinGrid(x, y) ? null : new Vector2Int(x, y);
    }

    public PathNode GetRandomNode(bool onlyWalkable)
    {
        int index;

        do
        {
            index = UnityEngine.Random.Range(0, _gridArray.Length);
        } while (onlyWalkable && !_gridArray[index].IsWalkable);

        return _gridArray[index];
    }

    public PathNode TopNeighbour(int x, int y)
    {
        y += 1;

        return GetNode(x, y);
    }

    public PathNode BottomNeighbour(int x, int y)
    {
        y -= 1;

        return GetNode(x, y);
    }

    public PathNode LeftNeighbour(int x, int y)
    {
        x -= 1;

        return GetNode(x, y);
    }

    public PathNode RightNeighbour(int x, int y)
    {
        x += 1;

        return GetNode(x, y);
    }

    private bool IsWithinGrid(int gridX, int gridY)
    {
        return gridX >= 0 && gridX < _gridSize.x && gridY >= 0 && gridY < _gridSize.y;
    }

    private int ToArrayIndex(int gridX, int gridY)
    {
        if (!IsWithinGrid(gridX, gridY))
        {
            return -1;
        }

        return gridY * _gridSize.x + gridX;
    }

    private void GetNodePosition(PathNode pathNode, out int x, out int y)
    {
        var index = Array.IndexOf(_gridArray, pathNode);

        if (index == -1)
        {
            x = -1;
            y = -1;
        } else 
        {
            x = index % _gridSize.x;
            y = index / _gridSize.x;
        }
    }

    private void CreateNodes()
    {
        _gridArray = new PathNode[_gridSize.x * _gridSize.y];
        for (var i = 0; i < _gridArray.Length; i++)
        {
            var x = i % _gridSize.x;
            var y = i / _gridSize.x;

            _gridArray[i] = new PathNode(x, y);
        }
    }

    private void SetWalkableNodes()
    {
        for (var i = 0; i < GetSize().x; i++)
        {
            for (var j = 0; j < GetSize().y; j++)
            {
                GetNode(i, j).IsWalkable = IsWalkable(new Vector2Int(i, j));
            }
        }
    }

    private bool IsWalkable(Vector2Int pos)
    {
        var gridCenter = GetGridCenter();
        var tile = objectsTileMap.GetTile(new Vector3Int(pos.x - gridCenter.x, pos.y - gridCenter.y, 0));

        return tile == null;
    }
}
