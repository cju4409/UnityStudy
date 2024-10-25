using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using UnityEngine;

// 델리게이트 사용
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

    // 커플링 문제: myMovement를 멤버변수로 사용함으로써 Picking과 Movement가 독립적 사용이 힘들어지고 커플이 되어버림
    // Delegate 사용으로 해결 가능
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

    //            //회전
    //            //Dot으로 내적 구함
    //            //float d = Vector3.Dot(transform.forward, dir);
    //            //역함수(y값으로 x값을 구함)로 라디안값 획득
    //            //float r = Mathf.Acos(d);
    //            //float angle = 180.0f * (r / Mathf.PI);
    //            // 내적 계산은 항상 내각으로 계산하므로 반시계로 회전 필요 시 right와 내적을 구해서 처리
    //            // 반시계 방향 회전시 마이너스 붙여서 반대방향으로 회전해줘야함
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

        //GetMouseButtonDown 0:왼쪽, 1:오른쪽, 2:휠
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
                //해당 컴포넌트의 모든 코루틴을 종료 => StopAllCoroutines();
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

    //코루틴 - 기본 IEnumerator로 반환
    //yield return을 만나기전까지 실행 후 다음 프레임에서 yierd return 후의 코드를 실행
    //StartCoroutine(함수())로 실행
    //무조건 yield return (조건 or null) 문장을 가져야함
    //멤버변수를 지역변수로 사용가능해 메모리 힙영역이 아닌 스택영역을 사용하므로 가비지 생성을 줄일 수 있음

}
