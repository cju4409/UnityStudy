using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Popup : MonoBehaviour
{
    public TMPro.TMP_Text myTitle;
    public TMPro.TMP_Text myContent;

    //Ű���� event : ��������Ʈ�� ���� �� privateó�� �ܺο��� �ʱ�ȭ, ��� �Ұ���. �߰�(+=)�� ������
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
