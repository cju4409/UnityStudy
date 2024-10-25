using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//상태기계 (유한 상태 기계) - 간단한 AI구현
public class Sensor : MonoBehaviour
{
    public enum State
    {
        Closed,
        Openning,
        Opened,
        Closing
    }
    void ChanageState(State s)
    {
        if (s == myState) return;
        myState = s;
        switch (myState)
        {
            case State.Closed:
                break;
            case State.Openning:
                //dir = openPos - myDoor.position;
                ////dist = Vector3.Distance(openPos, transform.position);
                //// Vector3의 magnitude = 거리값
                //dist = dir.magnitude;
                //// 길이가 1인 단위 벡터로 변환 Normalize();
                //dir.Normalize();

                CalculateDir(ref openPos);
                break;
            case State.Opened:
                break;
            case State.Closing:
                CalculateDir(ref closePos);
                break;
        }
    }
    void StateProcess()
    {
        switch (myState)
        {
            case State.Closed:
                break;
            case State.Openning:
                MoveDoor(State.Opened);
                break;
            case State.Opened:
                break;
            case State.Closing:
                MoveDoor(State.Closed);
                break;
        }
    }

    void CalculateDir(ref Vector3 targetPos)
    {
        dir = targetPos - myDoor.position;
        dist = dir.magnitude;
        dir.Normalize();
    }

    void MoveDoor(State done)
    {
        float delta = Time.deltaTime * 2.0f;
        if (delta > dist) delta = dist;
        myDoor.Translate(dir * delta);
        dist -= delta;
        if (Mathf.Approximately(dist, 0.0f)) ChanageState(done);
    }

    State myState = State.Closed;
    public LayerMask mask;
    public Transform myDoor;
    bool isEnter = false;
    float upDist = 0.0f;
    float speed = 3.0f;

    Vector3 dir, openPos, closePos;
    float dist;



    // Start is called before the first frame update
    void Start()
    {
        closePos = myDoor.position;
        openPos = myDoor.position + Vector3.up * 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        StateProcess();
        //if (isEnter)
        //{
        //    if (!Mathf.Approximately(3.0f, upDist))
        //    {
        //        float distY = speed * Time.deltaTime;
        //        if (distY + upDist > 3.0f) distY = 3.0f - upDist;
        //        myDoor.Translate(Vector3.up * distY);
        //        upDist += distY;
        //    }
        //}
        //else
        //{
        //    if (!Mathf.Approximately(0.0f, upDist))
        //    {
        //        float distY = -speed * Time.deltaTime;
        //        if (distY + upDist < 0.0f) distY = -upDist;
        //        myDoor.Translate(Vector3.up * distY);
        //        upDist += distY;
        //    }
        //}
    }


    void DoorUp()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if ((mask & 1 << other.gameObject.layer) == 0) return;
        ChanageState(State.Openning);
        isEnter = true;
        //myDoor.Translate(dir * Time.deltaTime);
    }
    private void OnTriggerStay(Collider other)
    {
    }
    private void OnTriggerExit(Collider other)
    {
        if ((mask & 1 << other.gameObject.layer) == 0) return;
        ChanageState(State.Closing);
        isEnter = false;
        //myDoor.Translate(-dir * Time.deltaTime);
    }

}
