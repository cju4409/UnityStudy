using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class SpringArm : MonoBehaviour
{
    public LayerMask crashMask;

    public float rotSpeed = 3.0f;
    public Vector2 rotRange = new Vector2(-20, 60);
    Vector3 rotAngle = Vector3.zero;

    public float zoomSpeed = 3.0f;
    public Vector2 zoomRange = new Vector2(-7, -1);
    float camDist, desireDist = 0.0f;

    Transform _cam;
    public Transform myCam
    {
        get
        {
            if (_cam == null)
            {
                Camera cam = transform.GetComponentInChildren<Camera>();
                if (cam != null)
                {
                    _cam = cam.transform;
                }
            }
            return _cam;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rotAngle = transform.localRotation.eulerAngles;
        desireDist = camDist = myCam.localPosition.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            float rotY = -Input.GetAxis("Mouse Y");
            rotAngle.x += rotY * rotSpeed;
            //if (rotAngle.x > rotRange.y) rotAngle.x = rotRange.y;
            //if (rotAngle.x < rotRange.x) rotAngle.x = rotRange.x;
            // Mathf.Clamp : 세팅하고자 하는 변수, 최소값, 최대값을 매개 변수로 범위내로 조정해줌
            rotAngle.x = Mathf.Clamp(rotAngle.x, rotRange.x, rotRange.y);

            float rotX = Input.GetAxis("Mouse X");
            //rotAngle.y += rotX * rotSpeed;
            transform.parent.Rotate(Vector3.up * rotX * rotSpeed);


            transform.localRotation = Quaternion.Euler(rotAngle);
        }

        float wheel = Input.GetAxis("Mouse ScrollWheel");
        float offset = 0.5f;
        desireDist = Mathf.Clamp(desireDist + wheel, zoomRange.x, zoomRange.y);
        camDist = Mathf.Lerp(camDist, desireDist, Time.deltaTime * 5.0f);
        if (Physics.Raycast(transform.position, -transform.forward, out RaycastHit hit,
            -camDist + offset, crashMask))
        {
            camDist = -hit.distance + offset;
        }
        myCam.localPosition = new Vector3(0, 0, camDist);
    }
}
