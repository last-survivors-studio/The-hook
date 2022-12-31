using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterNoise : MonoBehaviour
{

    public float power = 3.0f;
    public float scale = 1.0f;
    public float timeScale = 1.0f;

    public float waveForce = 1.0f;

    private float offsetX;
    private float offsetY;
    private MeshFilter mf;

    // Start is called before the first frame update
    void Start()
    {
        mf = GetComponent<MeshFilter>();
        MakeNoise();
    }

    // Update is called once per frame
    void Update()
    {
        MakeNoise();
        offsetX += Time.deltaTime + timeScale;
        if(offsetY <= 0.1) offsetY += Time.deltaTime * timeScale;
        if (offsetY >= power) offsetY -= Time.deltaTime * timeScale;
    }

    void MakeNoise()
    {
        Vector3[] vertex = mf.mesh.vertices;

        for (int i=0; i < vertex.Length; i++)
        {
            vertex[i].y = CalculateHeight(vertex[i].x, vertex[i].z * power);
            vertex[i].y *= waveForce;
        }

        mf.mesh.vertices = vertex;
    }

    float CalculateHeight(float x, float y)
    {
        float xCord = x * scale + offsetX;
        float yCord = y * scale + offsetY;

        return Mathf.PerlinNoise(xCord, yCord);
    }
}
