using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2D : Movement2D
{

    // Update is called once per frame
    void Update()
    {
        // GetAxis = 아날로그값. 레버. 천천히 감속하면서 변함 ex) 0.56214 => 0.65892 => 1.0
        // GetAxisRaw = 디지털값. -1,0,1. 감속없이 확변함
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
