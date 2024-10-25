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
            //Raycast�� ������ �����������̾����
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

    // �ε����� ����
    //private void OnCollisionEnter(Collision collision)
    //{
    //    // �±� ��� �� string���¿��� ���� �߻� �� ������ϱ� ���� ��õ���� ����.
    //    if (collision.gameObject.CompareTag("Destroy"))
    //    {
    //        DestroyObject();
    //    }

    //}

    // �ε����� ��
    private void OnCollisionStay(Collision collision)
    {



    }


    // �ε����� ������
    private void OnCollisionExit(Collision collision)
    {


    }
}
