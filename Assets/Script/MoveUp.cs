using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveUp : MonoBehaviour
{
    float movedDist;
    Vector3 dir = Vector3.up;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float delta = Time.deltaTime;
        if(movedDist + delta > 3.0f)
        {
            delta = 3.0f - movedDist;
        }
        movedDist += delta;
        if(Mathf.Approximately(movedDist, 3.0f))
        {
            movedDist = 0.0f;
            dir = -dir;
        }

        //transform.position = transform.position + dir * delta;
        transform.Translate(dir * delta);

        //태양
        //지구 공전 365일 자전 1일 걸림
        //달
        //1초가 1일
    }
}
