using UnityEngine;

public class MeshUtils
{
    private static Quaternion[] cachedQuaternionEulerArr;

    private static Mesh CreateEmptyMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = new Vector3[0];
        mesh.uv = new Vector2[0];
        mesh.triangles = new int[0];
        return mesh;
    }

    public static void CreateEmptyMeshArrays(int quadCount, out Vector3[] vertices, out Vector2[] uvs, out int[] triangles)
    {
        vertices = new Vector3[4 * quadCount];
        uvs = new Vector2[4 * quadCount];
        triangles = new int[6 * quadCount];
    }

    public static void AddToMeshArrays(Vector3[] vertices, Vector2[] uvs, int[] triangles, int index, Vector3 pos, Vector3 baseSize, float uv)
    {
        int vIndex = index * 4;
        int vIndex0 = vIndex;
        int vIndex1 = vIndex + 1;
        int vIndex2 = vIndex + 2;
        int vIndex3 = vIndex + 3;


        vertices[vIndex0] = pos + new Vector3(-baseSize.x, baseSize.y);
        vertices[vIndex1] = pos + new Vector3(-baseSize.x, -baseSize.y);
        vertices[vIndex2] = pos + new Vector3(baseSize.x, -baseSize.y);
        vertices[vIndex3] = pos + baseSize;

        uvs[vIndex0] = new Vector2(uv, uv);
        uvs[vIndex1] = new Vector2(uv, uv);
        uvs[vIndex2] = new Vector2(uv, uv);
        uvs[vIndex3] = new Vector2(uv, uv);

        //Create triangles
        int tIndex = index * 6;

        triangles[tIndex + 0] = vIndex0;
        triangles[tIndex + 1] = vIndex3;
        triangles[tIndex + 2] = vIndex1;

        triangles[tIndex + 3] = vIndex1;
        triangles[tIndex + 4] = vIndex3;
        triangles[tIndex + 5] = vIndex2;
    }
}
