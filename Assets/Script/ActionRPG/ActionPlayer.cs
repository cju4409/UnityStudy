using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActionPlayer : BattleSystem
{
    public Rigidbody myRigid;
    public LayerMask enemyMask;
    float targetX, targetY, curX, curY;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        targetX = Input.GetAxis("Horizontal");
        targetY = Input.GetAxis("Vertical");

        curX = Mathf.Lerp(curX, targetX, Time.deltaTime * 10.0f);
        curY = Mathf.Lerp(curY, targetY, Time.deltaTime * 10.0f);

        myAnim.SetFloat("x", curX);
        myAnim.SetFloat("y", curY);

        //A:지점1, B:지점2
        // 보간식: A + (B-A) * T(시간)
        //if (Input.GetKeyDown(KeyCode.Space) && !myAnim.GetBool(animData.IsRoll))
        //{
        //    //Vector3 dir = new Vector3(x, 0, y);
        //    //float angle = Vector3.Angle(Vector3.forward, dir);
        //    //float rot = Vector3.Dot(Vector3.right, dir) < 0 ? -1.0f : 1.0f;
        //    //transform.Rotate(Vector3.up * angle * rot);
        //    myAnim.SetTrigger(animData.OnRoll);
        //}

        // 점프 구현
        if (Input.GetKeyDown(KeyCode.Space) && !myAnim.GetBool("IsAir"))
        {
            myAnim.SetTrigger(animData.OnRoll);
            //AddForce : 정한 방향으로 강제로 힘을 줌
            //myRigid?.AddForce(Vector3.up * 300.0f);
            StartCoroutine(Jumping(1.0f, 2.0f));
        }

        // EventSystem.current.IsPointerOverGameObject : 현재 마우스 포인터가 UI위에 올라가있는지 아닌지 bool값을 리턴함
        if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0) && !myAnim.GetBool(animData.IsAttack))
        {
            myAnim.SetTrigger(animData.OnAttack);
        }

        if (Input.GetKeyDown(KeyCode.F1))
        {
            OnSkill();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            //SetActive:활성/비활성 상태로 전환
            Inventory.Instance.gameObject.SetActive(!Inventory.Instance.gameObject.activeSelf);
        }
    }

    public void OnSkill()
    {
        myAnim.SetTrigger(animData.OnJumpAttack);
    }

    public new void OnAttack()
    {
        //Overlap : 특정 영역안에 오버랩된 오브젝트들과의 충돌을 불러옴
        Collider[] list = Physics.OverlapSphere(transform.position + transform.forward, 1.0f, enemyMask);
        if(list != null)
        {
            foreach (Collider col in list)
            {
                IBattle ibat = col.GetComponent<IBattle>();
                if(ibat != null && ibat.IsLive)
                {
                    ibat.OnDamage(30.0f);
                }
            }
        }
        
    }
    public void OnJumpAttack()
    {
        Collider[] list = Physics.OverlapSphere(transform.position, 2.0f, enemyMask);
        if(list != null)
        {
            foreach (Collider col in list) {
                IBattle ibat = col.GetComponent<IBattle>();
                if (ibat != null && ibat.IsLive)
                {
                    ibat.OnDamage(100.0f);
                }
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            myAnim.SetBool("IsAir", true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            myAnim.SetBool("IsAir", false);
        }
    }

    IEnumerator Jumping(float t, float jumpHeight)
    {
        Vector3 orgPos = transform.position;
        float curHeight = 0.0f;
        float orgT = t;
        while (t > 0.0f)
        {
            t -= Time.deltaTime;
            curHeight = Mathf.Sin(((orgT - t) / orgT) * Mathf.PI) * jumpHeight;
            transform.position = orgPos + Vector3.up * curHeight;
            yield return null;
        }
        transform.position = orgPos;
    }
}
