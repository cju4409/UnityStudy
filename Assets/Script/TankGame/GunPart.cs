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
        // 회전을 허수(쿼터니언)로 저장하는 이유 : 짐벌락(축 회전시 다른 축이 겹쳐져버리는현상)을 막기위해서 ** 항상 쿼터니언값을 오일러값으로 변환해줘야함
        // 무조건 양의 값으로 저장되므로 필요시 음의 값으로 변환해줘야함.
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
