using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class AIMovement : Movement
{
    NavMeshPath _path = null;
    NavMeshPath path
    {
        get
        {
            if (_path == null) _path = new NavMeshPath();
            return _path;
        }
    }
    Coroutine aiMove;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public new void OnMove(Vector3 pos)
    {
        OnMove(pos, null);
    }

    public new void OnMove(Vector3 pos, UnityAction act)
    {
        // -1은 비트로 나타낼시 11111111111... 이라서 비트연산에서 모두 1을 의미함 (맨앞자리는 0이면 양수 1이면 음수를 의미) = NavMesh.AllAreas
        NavMesh.CalculatePath(transform.position, pos, NavMesh.AllAreas, path);
        switch (path.status)
        {
            case NavMeshPathStatus.PathInvalid: //계산 실패
                act?.Invoke();
                break;
            case NavMeshPathStatus.PathPartial: //목적지까지 갈 수 없음
            case NavMeshPathStatus.PathComplete: //계산 성공
                if (aiMove != null) StopCoroutine(aiMove);
                aiMove = StartCoroutine(MovingByPath(path.corners, act));
                break;
        }
    }

    IEnumerator MovingByPath(Vector3[] conners, UnityAction act)
    {
        int curPos = 0;
        while (++curPos < conners.Length)
        {
            yield return base.OnMove(conners[curPos]);
        }
        act?.Invoke();
        aiMove = null;
    }

    protected new void OnFollow(Transform target)
    {
        if (aiMove != null) StopCoroutine(aiMove);
        aiMove = StartCoroutine(Following(target));
    }

    IEnumerator Following(Transform target)
    {
        while (target != null)
        {
            NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, path);
            switch (path.status)
            {
                case NavMeshPathStatus.PathInvalid:
                    break;
                case NavMeshPathStatus.PathPartial:
                case NavMeshPathStatus.PathComplete:
                    {
                        if (path.corners.Length > 1)
                        {
                            myAnim.SetBool(animData.IsMoving, true);

                            Vector3 targetPos = path.corners[1];
                            targetPos.y = transform.position.y;
                            Vector3 dir = targetPos - transform.position;

                            //Vector3 dir = path.corners[1] - transform.position;
                            float dist = dir.magnitude;
                            float delta;

                            if (dist > battleStat.AttackRange)
                            {
                                dir.Normalize();
                                delta = Time.deltaTime * moveSpeed;
                                if (delta > dist) delta = dist;
                                transform.Translate(dir * delta, Space.World);
                                dist -= delta;
                            }
                            else
                            {
                                if(playTime >= battleStat.AttackDelay)
                                {
                                    playTime = 0.0f;
                                    myAnim.SetTrigger(animData.OnAttack);
                                }
                            }

                            //if (delta > dist) delta = dist;
                            //transform.Translate(dir * delta, Space.World);
                            //dist -= delta;

                            float angle = Vector3.Angle(transform.forward, dir);
                            float rotDir = Vector3.Dot(transform.right, dir) < 0 ? -1.0f : 1.0f;
                            delta = Time.deltaTime * rotSpeed;
                            if (delta > angle) delta = angle;
                            transform.Rotate(Vector3.up * rotDir * delta);
                            angle -= delta;
                        }
                        else
                        {
                            myAnim.SetBool(animData.IsMoving, false);
                        }
                    }
                    break;
            }

            yield return null;
        }
    }
}
