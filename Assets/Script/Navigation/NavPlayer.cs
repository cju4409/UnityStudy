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
        //길찾기해서 이동가능한 최대의 범위까지만 이동함
        //transform.position = pos;

        //Warp함수:길찾기와 상관 없이 이동가능하게 해줌
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
            // 이동
            float delta = Time.deltaTime * myAgent.speed;
            if (delta > dist) delta = dist;
            transform.Translate(dir * delta, Space.World);
            dist -= delta;
            // 회전
            delta = Time.deltaTime * myAgent.angularSpeed;
            if (delta > angle) delta = angle;
            transform.Rotate(Vector3.up * delta * rotDir);
            angle -= delta;
            yield return null;
        }
    }

    IEnumerator Moving(Vector3 pos)
    {
        //둘다 동일한 이동 방법
        //myAgent.SetDestination(pos);
        myAgent.destination = pos;
        myAnim.SetBool(animData.IsMoving, true);

        //길찾기 연산은 비동기 연산이라 여러 프레임을 걸쳐서 계산해서 코루틴에서 일정기간 remainingDistance값이 0으로 잡히게됨
        //고로 pathPending( : 계산하고 있는지 아닌지를 체크하는 파라미터 )를 이용하여 처리함
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
