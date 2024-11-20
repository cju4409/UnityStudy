using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomMesh : MonoBehaviour
{
    public MeshFilter myMesh;
    Vector3 CalNormal(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        Vector3 v1 = p1 - p2;
        Vector3 v2 = p3 - p2;
        return Vector3.Cross(v2, v1).normalized;
    }

    // Start is called before the first frame update
    void Start()
    {
        Vector3[] vertices = new Vector3[16];
        //vertices[0] = new Vector3(-0.5f, 0.0f, -0.5f);
        //vertices[1] = new Vector3(-0.5f, 0.0f, 0.5f);
        //vertices[2] = new Vector3(0.5f, 0.0f, 0.5f);
        //vertices[3] = new Vector3(0.5f, 0.0f, -0.5f);
        //vertices[4] = new Vector3(0.0f, 0.5f, 0.0f);


        //left side
        vertices[0] = new Vector3(-0.5f, 0.0f, -0.5f);
        vertices[1] = new Vector3(-0.5f, 0.0f, 0.5f);
        vertices[2] = new Vector3(0.0f, 0.75f, 0.0f);

        //back side
        vertices[3] = new Vector3(-0.5f, 0.0f, 0.5f);
        vertices[4] = new Vector3(0.5f, 0.0f, 0.5f);
        vertices[5] = new Vector3(0.0f, 0.75f, 0.0f);

        //right side
        vertices[6] = new Vector3(0.5f, 0.0f, 0.5f);
        vertices[7] = new Vector3(0.5f, 0.0f, -0.5f);
        vertices[8] = new Vector3(0.0f, 0.75f, 0.0f);

        //forward side
        vertices[9] = new Vector3(0.5f, 0.0f, -0.5f);
        vertices[10] = new Vector3(-0.5f, 0.0f, -0.5f);
        vertices[11] = new Vector3(0.0f, 0.75f, 0.0f);
        
        //botom side
        vertices[12] = new Vector3(-0.5f, 0.0f, -0.5f);
        vertices[13] = new Vector3(-0.5f, 0.0f, 0.5f);
        vertices[14] = new Vector3(0.5f, 0.0f, 0.5f);
        vertices[15] = new Vector3(0.5f, 0.0f, -0.5f);

        myMesh.mesh.vertices = vertices;

        //int[] indices = new int[]
        //{
        //    0, 1, 4,
        //    1, 2, 4,
        //    2, 3, 4,
        //    3, 0, 4,
        //    3, 2, 1,
        //    0, 3, 1,
        //};
        int[] indices = new int[]
        {
            0, 1, 2,
            3, 4, 5,
            6, 7, 8,
            9, 10, 11,
            12, 14, 13,
            12, 15, 14
        };
        myMesh.mesh.triangles = indices;

        Vector2[] uv = new Vector2[vertices.Length];
        //uv[0] = new Vector2(0.0f, 0.0f);
        //uv[1] = new Vector2(0.0f, 1.0f);
        //uv[2] = new Vector2(1.0f, 1.0f);
        //uv[3] = new Vector2(1.0f, 0.0f);
        //uv[4] = new Vector2(0.5f, 0.5f);

        uv[0] = new Vector2(0.0f, 0.0f);
        uv[1] = new Vector2(0.0f, 1.0f);
        uv[2] = new Vector2(0.5f, 0.5f);

        uv[3] = new Vector2(0.0f, 1.0f);
        uv[4] = new Vector2(1.0f, 1.0f);
        uv[5] = new Vector2(0.5f, 0.5f);

        uv[6] = new Vector2(1.0f, 1.0f);
        uv[7] = new Vector2(1.0f, 0.0f);
        uv[8] = new Vector2(0.5f, 0.5f);

        uv[9] = new Vector2(1.0f, 0.0f);
        uv[10] = new Vector2(0.0f, 0.0f);
        uv[11] = new Vector2(0.5f, 0.5f);


        uv[12] = new Vector2(0.0f, 0.0f);
        uv[13] = new Vector2(0.0f, 1.0f);
        uv[14] = new Vector2(1.0f, 1.0f);
        uv[15] = new Vector2(1.0f, 0.0f);

        myMesh.mesh.uv = uv;
        Vector3[] normals = new Vector3[vertices.Length];

        for (int i = 0; i < indices.Length; i += 3)
        {
            Vector3 normal = CalNormal(vertices[indices[i]], vertices[indices[i+1]], vertices[indices[i+2]]);
            normals[indices[i]] = normals[indices[i + 1]] = normals[indices[i + 2]] = normal;
        }

        myMesh.mesh.normals = normals;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
