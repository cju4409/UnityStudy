using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotion : AnimatorProperty
{
    public Transform myRoot;
    Vector3 deltaPos = Vector3.zero;
    Quaternion deltaRot = Quaternion.identity;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnAnimatorMove()
    {

        deltaPos += myAnim.deltaPosition;
        // 쿼터니언은 더하는것이 없고 곱한다는게 곱한량의 각도만큼 회전한다는 것임
        deltaRot *= myAnim.deltaRotation;
    }

    //FixedUpdata : 물리엔진과 동기화 되어있는 업데이트, CPU작업과 그래픽카드의 작업의 비동기에 대한 불일치를 그래픽카드에서만 작업하도록해서 해소함
    //오브젝트 충돌시 비동기로 인해 덜컹덜컹 거림을 없앰
    private void FixedUpdate()
    {
        myRoot.position += deltaPos;
        myRoot.rotation *= deltaRot;
        deltaPos = Vector3.zero;
        deltaRot = Quaternion.identity;
    }
}
