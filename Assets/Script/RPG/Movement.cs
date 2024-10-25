using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;


public class Movement : BattleSystem
{
    public float moveSpeed = 1.0f;
    public float rotSpeed = 720.0f;
    Coroutine move = null, rotate = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    protected void OnStop()
    {
        if (move != null) StopCoroutine(move);
        if (rotate != null) StopCoroutine(rotate);
        move = rotate = null;
    }

    public Coroutine OnMove(Vector3 targetPos)
    {
        return OnMove(targetPos, null);
    }

    public Coroutine OnMove(Vector3 targetPos, UnityAction done)
    {
        Vector3 dir = targetPos - transform.position;
        OnStop();
        move = StartCoroutine(Moving(dir, done));
        rotate = StartCoroutine(Rotating(dir.normalized));
        return move;
    }

    IEnumerator Moving(Vector3 dir, UnityAction done)
    {
        myAnim.SetBool(animData.IsMoving, true);
        float dist = dir.magnitude;
        dir.Normalize();

        while (dist > 0.0f)
        {
            if (!myAnim.GetBool(animData.IsAttack))
            {
                float delta = 3.0f * Time.deltaTime;
                if (delta > dist) delta = dist;
                transform.Translate(dir * delta, Space.World);
                dist -= delta;
            }
            //yield return (조건 or null) : 조건만큼 지연 후 반환
            //while문 안에서 사용 시 매 프레임 마다 한번씩 실행
            yield return null;
        }
        myAnim.SetBool(animData.IsMoving, false);
        done?.Invoke();
    }

    IEnumerator Rotating(Vector3 dir)
    {
        float angle = Vector3.Angle(transform.forward, dir);
        float rotDir = 1.0f;
        if (Vector3.Dot(transform.right, dir) < 0.0f) rotDir = -rotDir;
        while (angle > 0.0f)
        {
            float deltaAngle = 600.0f * Time.deltaTime;
            if (deltaAngle > angle) deltaAngle = angle;
            transform.Rotate(Vector3.up * deltaAngle * rotDir);
            angle -= deltaAngle;
            yield return null;
        }
    }

    public void OnFollow(Transform target)
    {
        OnStop();
        move = StartCoroutine(Following(target));
    }

    IEnumerator Following(Transform target)
    {
        int count = 0;
        while (target != null)
        {
            playTime += Time.deltaTime;
            myAnim.SetBool(animData.IsMoving, true);
            Vector3 dir = target.position - transform.position;
            float dist = dir.magnitude;

            float delta = 0.0f;
            if (dist > battleStat.AttackRange && !myAnim.GetBool(animData.IsAttack))
            {
                dir.Normalize();

                delta = Time.deltaTime * moveSpeed;
                if (delta > dist) delta = dist;
                transform.Translate(dir * delta, Space.World);
            }
            else
            {
                myAnim.SetBool(animData.IsMoving, false);
                if (playTime >= battleStat.AttackDelay)
                {
                    count++;
                    Debug.Log("공격!" + count);
                    //애니메이션에서 트리거 사용
                    myAnim.SetTrigger(animData.OnAttack);
                    playTime = 0.0f;
                }
            }

            delta = Time.deltaTime * rotSpeed;
            float angle = Vector3.Angle(transform.forward, dir.normalized);
            float rotDir = Vector3.Dot(transform.right, dir.normalized) >= 0 ? 1.0f : -1.0f;
            if (delta > angle) delta = angle;
            transform.Rotate(Vector3.up * delta * rotDir);

            yield return null;
        }
    }
}
