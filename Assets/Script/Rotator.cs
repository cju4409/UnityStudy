using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotSpeed = 360.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 1�ʿ� 360�� ȸ����Ŵ
        transform.Rotate(Vector3.up * Time.deltaTime * rotSpeed);
    }
}
