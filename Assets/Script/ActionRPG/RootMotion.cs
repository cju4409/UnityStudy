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
        // ���ʹϾ��� ���ϴ°��� ���� ���Ѵٴ°� ���ѷ��� ������ŭ ȸ���Ѵٴ� ����
        deltaRot *= myAnim.deltaRotation;
    }

    //FixedUpdata : ���������� ����ȭ �Ǿ��ִ� ������Ʈ, CPU�۾��� �׷���ī���� �۾��� �񵿱⿡ ���� ����ġ�� �׷���ī�忡���� �۾��ϵ����ؼ� �ؼ���
    //������Ʈ �浹�� �񵿱�� ���� ���ȴ��� �Ÿ��� ����
    private void FixedUpdate()
    {
        myRoot.position += deltaPos;
        myRoot.rotation *= deltaRot;
        deltaPos = Vector3.zero;
        deltaRot = Quaternion.identity;
    }
}
