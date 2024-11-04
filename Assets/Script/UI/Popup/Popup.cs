using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Popup : MonoBehaviour
{
    public TMPro.TMP_Text myTitle;
    public TMPro.TMP_Text myContent;

    //키워드 event : 델리게이트에 붙일 시 private처럼 외부에서 초기화, 사용 불가능. 추가(+=)는 가능함
    //public UnityAction closeAlarm;
    public event UnityAction closeAlarm;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClose()
    {
        closeAlarm?.Invoke();
        Destroy(gameObject);
    }
}
