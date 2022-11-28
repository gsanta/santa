using UnityEngine;

public class GridVisual : MonoBehaviour
{
    [SerializeField] private PathGrid _grid;

    private Mesh _mesh;
    
    private void Awake()
    {
        _mesh = new Mesh();
        
        GetComponent<MeshFilter>().mesh = _mesh;
    }

    private void Start()
    {
        Render();
    }

    private void Render()
    {
        MeshUtils.CreateEmptyMeshArrays(_grid.GetSize().x * _grid.GetSize().y, out Vector3[] vertices, out Vector2[] uv, out int[] triangles);

        for (int x = 0; x < _grid.GetSize().x; x++)
        {
            for (int y = 0; y < _grid.GetSize().y; y++)
            {
                RenderQuad(x, y, vertices, uv, triangles);
            }
        }

        _mesh.vertices = vertices;
        _mesh.uv = uv;
        _mesh.triangles = triangles;
    }

    private void RenderQuad(int x, int y, Vector3[] vertices, Vector2[] uvs, int[] triangles)
    {
        int index = y * _grid.GetSize().x + x;
        Vector2 quadSize = new Vector3(1, 1) * _grid.GetCellSize() / 2.0f * 0.9f;

        var pos = _grid.GetWorldPosition(x, y);
        var pos3d = new Vector3(pos.x, pos.y, -0.5f);

        float uv = _grid.GetNode(x, y).IsWalkable ? 0.1f : 0.3f;

        MeshUtils.AddToMeshArrays(vertices, uvs, triangles, index, pos3d, quadSize, uv);
    }
}
