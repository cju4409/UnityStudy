using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using UnityEngine;

// ��������Ʈ ���
using UnityEngine.Events;

public class Picking : MonoBehaviour
{
    enum State
    {
        Stop,
        Moving
    }
    State myState = State.Stop;

    public LayerMask moveMask;
    public LayerMask attackMask;
    public UnityEvent<Vector3> moveAction;
    public UnityEvent<GameObject> attackAction;

    // Ŀ�ø� ����: myMovement�� ��������� ��������ν� Picking�� Movement�� ������ ����� ��������� Ŀ���� �Ǿ����
    // Delegate ������� �ذ� ����
    //public Movement myMovement;

    //void ChangeState(State s)
    //{
    //    myState = s;
    //    switch (s)
    //    {
    //        case State.Stop:
    //            break;
    //        case State.Moving:
    //            dir = endPoint - transform.position;
    //            dist = dir.magnitude;
    //            dir.Normalize();

    //            //ȸ��
    //            //Dot���� ���� ����
    //            //float d = Vector3.Dot(transform.forward, dir);
    //            //���Լ�(y������ x���� ����)�� ���Ȱ� ȹ��
    //            //float r = Mathf.Acos(d);
    //            //float angle = 180.0f * (r / Mathf.PI);
    //            // ���� ����� �׻� �������� ����ϹǷ� �ݽð�� ȸ�� �ʿ� �� right�� ������ ���ؼ� ó��
    //            // �ݽð� ���� ȸ���� ���̳ʽ� �ٿ��� �ݴ�������� ȸ���������
    //            reqAngle = Vector3.Angle(transform.forward, dir);
    //            rotDir = 1.0f;
    //            if (Vector3.Dot(transform.right, dir) < 0.0f) rotDir = -rotDir;


    //            break;
    //    }
    //}
    //void StateProcess()
    //{
    //    switch (myState)
    //    {
    //        case State.Stop:
    //            break;
    //        case State.Moving:
    //            if (dist > 0.0f)
    //            {
    //                float delta = 3.0f * Time.deltaTime;
    //                if (delta > dist)
    //                {
    //                    delta = dist;
    //                    ChangeState(State.Stop);
    //                }
    //                transform.Translate(dir * delta, Space.World);


    //                dist -= delta;
    //            }

    //            if (reqAngle > 0.0f) {
    //                float deltaAngle = 600.0f * Time.deltaTime;
    //                if (deltaAngle > reqAngle) deltaAngle = reqAngle;
    //                transform.Rotate(Vector3.up * deltaAngle * rotDir);
    //                reqAngle -= deltaAngle;
    //            }
    //            break;
    //    }
    //}

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        //GetMouseButtonDown 0:����, 1:������, 2:��
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, attackMask))
            {
                attackAction?.Invoke(hit.transform.gameObject);
            }
            else if (Physics.Raycast(ray, out hit, Mathf.Infinity, moveMask))
            {
                moveAction?.Invoke(hit.point);
                //�ش� ������Ʈ�� ��� �ڷ�ƾ�� ���� => StopAllCoroutines();
                //Vector3 dir = hit.point - transform.position;
                //if (rotate != null) StopCoroutine(rotate);
                //rotate = StartCoroutine(Rotating(dir));
                //if (move != null) StopCoroutine(move);
                //move = StartCoroutine(Moving(dir));
                //endPoint = hit.point;
                //ChangeState(State.Moving);
            }
        }
    }

    //�ڷ�ƾ - �⺻ IEnumerator�� ��ȯ
    //yield return�� ������������ ���� �� ���� �����ӿ��� yierd return ���� �ڵ带 ����
    //StartCoroutine(�Լ�())�� ����
    //������ yield return (���� or null) ������ ��������
    //��������� ���������� ��밡���� �޸� �������� �ƴ� ���ÿ����� ����ϹǷ� ������ ������ ���� �� ����

}
