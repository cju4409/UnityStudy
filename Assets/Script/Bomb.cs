using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject Effect;
    bool isFire = false;
    public LayerMask crashMask;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isFire)
        {
            float dist = 5.0f * Time.deltaTime;
            //Raycast는 무조건 월드포지션이어야함
            if (Physics.Raycast(transform.position, transform.forward,
                out RaycastHit hit, dist, crashMask))
            {
                DestroyObject(hit.point);
            }
            transform.Translate(Vector3.forward * dist);
        }

    }

    public void OnFire()
    {
        isFire = true;
        transform.parent = null;
    }

    private void DestroyObject(Vector3 pos)
    {
        GameObject obj = Instantiate(Effect);
        obj.transform.position = pos;
        Destroy(gameObject);
    }

    // 부딛히기 시작
    //private void OnCollisionEnter(Collision collision)
    //{
    //    // 태그 사용 시 string형태여서 에러 발생 시 디버깅하기 힘들어서 추천하지 않음.
    //    if (collision.gameObject.CompareTag("Destroy"))
    //    {
    //        DestroyObject();
    //    }

    //}

    // 부딛히는 중
    private void OnCollisionStay(Collision collision)
    {



    }


    // 부딛히고 떨어짐
    private void OnCollisionExit(Collision collision)
    {


    }
}
