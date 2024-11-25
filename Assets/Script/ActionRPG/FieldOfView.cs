
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [Range(0.0f, 360.0f)]
    public float viewAngle = 90.0f;
    public float viewDistance = 3.0f;
    public int detailCount = 30;
    public MeshFilter myMesh;
    public LayerMask crashMask;
    public Vector3[] dirList;
    // Start is called before the first frame update
    void Start()
    {


        myMesh.mesh = new Mesh();
        Vector3[] vb = new Vector3[detailCount + 1];
        dirList = new Vector3[detailCount];
        vb[0] = new Vector3(0, 0, 0);
        dirList[0] = Quaternion.Euler(0, -viewAngle * 0.5f, 0) * Vector3.forward * viewDistance;


        vb[1] = vb[0] + dirList[0];
        float deltaAngle = viewAngle / (float)(detailCount - 1);

        for (int i = 2; i < vb.Length; i++)
        {
            dirList[i - 1] = Quaternion.Euler(0, deltaAngle, 0) * dirList[i - 2];
            vb[i] = vb[0] + dirList[i - 1];
        }

        myMesh.mesh.vertices = vb;

        int[] ib = new int[(detailCount - 1) * 3];

        for (int i = 0, j = 1; i < ib.Length; i += 3, j++)
        {
            ib[i] = 0;
            ib[i + 1] = j;
            ib[i + 2] = j + 1;
        }

        myMesh.mesh.triangles = ib;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3[] vb = myMesh.mesh.vertices;

        for (int i = 0; i < dirList.Length; i++)
        {
            if (Physics.Raycast(transform.position, (transform.rotation * dirList[i]).normalized,
                out RaycastHit hit, viewDistance, crashMask))
            {
                vb[i + 1] = vb[0] + dirList[i].normalized * hit.distance;
            }
            else
            {
                vb[i + 1] = vb[0] + dirList[i].normalized * viewDistance;
            }
        }
        myMesh.mesh.vertices = vb;
    }
}
