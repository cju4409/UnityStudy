using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2D : Movement2D
{

    // Update is called once per frame
    void Update()
    {
        // GetAxis = �Ƴ��αװ�. ����. õõ�� �����ϸ鼭 ���� ex) 0.56214 => 0.65892 => 1.0
        // GetAxisRaw = �����а�. -1,0,1. ���Ӿ��� Ȯ����
        moveDir.x = Input.GetAxisRaw("Horizontal");

        OnUpdate();

        if (Input.GetKeyDown(KeyCode.Space) && !myAnim.GetBool("IsAir"))
        {
            OnJump();
        }

        if (Input.GetMouseButtonDown(0) && !myAnim.GetBool(animData.IsAttack))
        {
            myAnim.SetTrigger(animData.OnAttack);
        }

    }

}
