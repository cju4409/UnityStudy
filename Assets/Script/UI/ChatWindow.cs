using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatWindow : MonoBehaviour
{
    public enum CHANNEL
    {
        Normal, Party, Guild, Trade
    }
    (string, string)[] chatDrops = { ("ffffff", "�Ϲ�"), ("0000ff", "��Ƽ"), ("00ff00", "���"), ("ffaa00", "�ŷ�") };

    CHANNEL myChannel = CHANNEL.Normal;
    public Transform myContent;
    public TMPro.TMP_InputField myInput;
    public Scrollbar myScroll;
    //public Dropdown myDrop;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddChat(string text)
    {
        if (text != "")
        {
            ChatItem item = Instantiate(Resources.Load("Prefabs/UI/ChatItem") as GameObject,
                myContent).GetComponent<ChatItem>();
            Vector2 size = (item.transform as RectTransform).sizeDelta;
            size.x = (transform as RectTransform).sizeDelta.x - 21;
            (item.transform as RectTransform).sizeDelta = size;

            int dropSelect = (int)myChannel;
            text = $"<#{chatDrops[dropSelect].Item1}>[{chatDrops[dropSelect].Item2}] {text}</color>";

            item.SetText(text);

            myInput.text = string.Empty;

            //ActivateInputField: �ٽ� ��Ŀ���� �����
            myInput.ActivateInputField();

            //��ũ���� ���� ������ ������. ������ Instantiate(������Ʈ ����)�� �������ӿ� �����°� �ƴ϶� �ڷ�ƾ���� ��� �� ����
            //myScroll.value = 0;
            StartCoroutine(ScrollZero());
        }
    }

    public void OnSelectDrop(int selectOption)
    {
        myChannel = (CHANNEL)selectOption;
    }

    IEnumerator ScrollZero()
    {
        yield return new WaitForSeconds(0.1f);
        myScroll.value = 0;
    }

}
