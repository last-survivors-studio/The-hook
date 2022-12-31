using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMovement : MonoBehaviour
{

    public float cellsize = 1.0f;
    public int gridSize = 16;
    public Vector3 offset = new Vector3 ( 0, 0, 0 );
    private MeshFilter filter;


    // Start is called before the first frame update
    void Start()
    {
        filter = GetComponent<MeshFilter>();
        filter.mesh = GenerateMesh();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private Mesh GenerateMesh()
    {
        Mesh m = new Mesh();

        var vertex = new Vector3[(gridSize+1)* (gridSize + 1)];
        var normals = new List<Vector3>();
        var uvs = new List<Vector2>(); //Flat plane

        float cellSize = 5;
        int v = 0;
        for (int x = 0; x <= gridSize; x++)
        {
            for (int y = 0; y <= gridSize; y++)
            {
                vertex[v]=(new Vector3(x* cellSize + offset.x, 0+ offset.y, y * cellSize+ offset.z));
                normals.Add(Vector3.up);
                uvs.Add(new Vector2(x / (float)gridSize, y / (float)gridSize));
                v++;
            }
        }

        var triangles = new int[gridSize * gridSize * 6];
        int ti = 0;
        int vi = 0;
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                triangles[ti] = vi;
                triangles[ti + 1] = triangles[ti + 4] = vi + 1;
                triangles[ti + 2] = triangles[ti + 3] = vi + gridSize + 1;
                triangles[ti + 5] = vi + gridSize + 2;
                ti += 6;
                vi++;
            }
            vi++;
        }
     

        m.vertices= vertex;
        m.SetNormals(normals);
        m.SetUVs(0, uvs);
        m.SetTriangles(triangles, 0);
       
        return m;
    }

}
