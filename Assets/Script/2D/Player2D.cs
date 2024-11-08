using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Player2D : BattleSystem2D
{
    public LayerMask myEnemy;
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


    public void OnAttack()
    {
        Vector2 dir = new Vector2(myRenderer.flipX ? -1.0f: 1.0f, 0.0f);
        Collider2D[] list = Physics2D.OverlapCircleAll((Vector2)transform.position + dir, 1.0f, myEnemy);
        foreach(Collider2D c in list)
        {
            c.GetComponent<IDamage>()?.OnDamage(battleStat.AP);
        }
    }

}
