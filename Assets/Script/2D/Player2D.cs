using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2D : SpriteProperty
{
    public Collider2D myColider;
    public float moveSpeed = 2.0f;
    SpriteRenderer render;
    public Rigidbody2D myRigid;
    // Start is called before the first frame update
    void Start()
    {
        render = transform.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // GetAxis = 아날로그값. 레버. 천천히 감속하면서 변함 ex) 0.56214 => 0.65892 => 1.0
        // GetAxisRaw = 디지털값. -1,0,1. 감속없이 확변함
        float x = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector3.right * x * moveSpeed * Time.deltaTime);

        if (Mathf.Approximately(x, 0.0f)) myAnim.SetBool("IsMoving", false);
        else
        {
            myAnim.SetBool("IsMoving", true);
            render.flipX = x < 0.0f ? true : false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !myAnim.GetBool("IsAir"))
        {
            StartCoroutine(Jumping());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground")) myAnim.SetBool("IsAir", false);
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

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground")) myAnim.SetBool("IsAir", true);
    }
}
