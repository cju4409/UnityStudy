using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // ȸ���� ���(���ʹϾ�)�� �����ϴ� ���� : ������(�� ȸ���� �ٸ� ���� ����������������)�� �������ؼ� ** �׻� ���ʹϾ��� ���Ϸ������� ��ȯ�������
        // ������ ���� ������ ����ǹǷ� �ʿ�� ���� ������ ��ȯ�������.
        Vector3 rot = transform.localRotation.eulerAngles;
        if (rot.x > 180.0f) rot.x -= 360.0f;
        float delta = Time.deltaTime * 90.0f;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rot.x -= delta;
            if (rot.x < -80.0f) rot.x = -80.0f;
            transform.localRotation = Quaternion.Euler(rot);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rot.x += delta;
            if (rot.x > 5.0f) rot.x = 5.0f;
            transform.localRotation = Quaternion.Euler(rot);
        }
    }
}
