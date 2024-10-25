using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public GameObject orgBomb;
    public Bomb myBomb;
    public Transform myMuzzle;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            //transform.position += Vector3.forward * Time.deltaTime * 2.0f;
            transform.Translate(Vector3.forward * Time.deltaTime * 2.0f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * Time.deltaTime * 2.0f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.down * Time.deltaTime * 180.0f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * Time.deltaTime * 180.0f);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myBomb.OnFire();
            //reload
            //부모를 정해주고 Instantiate하면 밑에 트랜스폼 초기화 코드가 필요하지 않음
            GameObject obj = Instantiate(orgBomb, myMuzzle);

            //GameObject obj = Instantiate(orgBomb);
            //obj.transform.parent = myMuzzle;
            //obj.transform.localPosition = Vector3.zero;
            //obj.transform.localRotation = Quaternion.identity;
            myBomb = obj.GetComponent<Bomb>();
        }
    }
}
