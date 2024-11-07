using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : SpriteProperty
{
    Collider2D _colider = null;
    public Collider2D myColider
    {
        get
        {
            if (_colider == null)
            {
                _colider = GetComponentInChildren<Collider2D>();
            }
            return _colider;
        }
    }
    Rigidbody2D _rigid = null;
    public Rigidbody2D myRigid
    {
        get
        {
            if (_rigid == null)
            {
                _rigid = GetComponentInChildren<Rigidbody2D>();
            }
            return _rigid;
        }
    }
    protected SpriteRenderer render;
    public float moveSpeed = 2.0f;


    protected Vector2 moveDir = new Vector2(0.0f, 0.0f);
    protected float deltaDist;

    protected virtual void OnCheckGround(Transform tr) { }

    protected void OnUpdate()
    {
        deltaDist = Time.deltaTime * moveSpeed;
        transform.Translate(moveDir * deltaDist);

        if (Mathf.Approximately(moveDir.x, 0.0f)) myAnim.SetBool(animData.IsMoving, false);
        else
        {
            myAnim.SetBool("IsMoving", true);
            if (render != null) render.flipX = moveDir.x < 0.0f ? true : false;
        }
    }


    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            myAnim.SetBool("IsAir", false);
            OnCheckGround(collision.transform);
        }
    }

    protected void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground")) myAnim.SetBool("IsAir", true);
    }

    protected void OnJump()
    {
        StopAllCoroutines();
        StartCoroutine(Jumping());
    }

    IEnumerator Jumping()
    {
        myRigid.AddForce(Vector2.up * 500.0f);
        // WaitForFixedUpdate : 물리 처리까지 기다림
        yield return new WaitForFixedUpdate();
        myColider.isTrigger = true;
        while (myRigid.velocity.y > 0.0f)
        {
            yield return null;
        }
        myColider.isTrigger = false;
    }

}
