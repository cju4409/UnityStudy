using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����: �������� (CPU�� �ڵ� ����, �б� ���� �ӵ� ���� ����, ����� ������ ����)
// ��: �ν��Ͻ� (����������, ������(����ī��Ʈ�� 0�̸� ������)�÷��Ͱ� ����)
// ������: static, ��� (���� : ���α׷��� ����, ���� : ���α׷��� ����)
// �ڵ�: �Լ�
public class Inventory : MonoBehaviour
{
    // static ������� Ŭ����: �׳� Ŭ������ �ν��Ͻ��� �����ϴ� ���� (�޸� �ּҰ��� �����һ�)
    public static Inventory Instance
    {
        get; private set;
    }
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
