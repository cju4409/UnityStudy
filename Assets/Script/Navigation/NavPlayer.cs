using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavPlayer : AnimatorProperty
{
    public NavMeshAgent myAgent;
    Coroutine move;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnTeleport(Vector3 pos)
    {
        //��ã���ؼ� �̵������� �ִ��� ���������� �̵���
        //transform.position = pos;

        //Warp�Լ�:��ã��� ��� ���� �̵������ϰ� ����
        myAgent.Warp(pos);
    }

    public void OnMove(Vector3 pos)
    {
        if (!myAgent.isOnOffMeshLink)
        {
            if (move != null) StopCoroutine(move);
            move = StartCoroutine(Moving(pos));
        }
    }

    IEnumerator Jumping(Vector3 dir, float dist)
    {
        float angle = Vector3.Angle(transform.forward, dir);
        float rotDir = Vector3.Dot(transform.right, dir) < 0.0f ? -1.0f : 1.0f;
        while (dist > 0.0f)
        {
            // �̵�
            float delta = Time.deltaTime * myAgent.speed;
            if (delta > dist) delta = dist;
            transform.Translate(dir * delta, Space.World);
            dist -= delta;
            // ȸ��
            delta = Time.deltaTime * myAgent.angularSpeed;
            if (delta > angle) delta = angle;
            transform.Rotate(Vector3.up * delta * rotDir);
            angle -= delta;
            yield return null;
        }
    }

    IEnumerator Moving(Vector3 pos)
    {
        //�Ѵ� ������ �̵� ���
        //myAgent.SetDestination(pos);
        myAgent.destination = pos;
        myAnim.SetBool(animData.IsMoving, true);

        //��ã�� ������ �񵿱� �����̶� ���� �������� ���ļ� ����ؼ� �ڷ�ƾ���� �����Ⱓ remainingDistance���� 0���� �����Ե�
        //��� pathPending( : ����ϰ� �ִ��� �ƴ����� üũ�ϴ� �Ķ���� )�� �̿��Ͽ� ó����
        while (myAgent.pathPending || myAgent.remainingDistance > myAgent.stoppingDistance)
        {
            if (myAgent.isOnOffMeshLink)
            {
                myAnim.SetTrigger(animData.OnJump);
                myAgent.isStopped = true;
                myAgent.velocity= Vector3.zero;
                Vector3 dir = myAgent.currentOffMeshLinkData.endPos - transform.position;
                float dist = dir.magnitude;
                dir.Normalize();
                yield return StartCoroutine(Jumping(dir, dist));

                myAgent.CompleteOffMeshLink();
                myAgent.isStopped = false;
            }

            yield return null;
        }
        myAnim.SetBool(animData.IsMoving, false);
    }
}
