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
    (string, string)[] chatDrops = { ("ffffff", "일반"), ("0000ff", "파티"), ("00ff00", "길드"), ("ffaa00", "거래") };

    CHANNEL myChannel = CHANNEL.Normal;
    public Transform myContent;
    public TMPro.TMP_InputField myInput;
    public Scrollbar myScroll;
    //public Dropdown myDrop;
    // Start is called before the first frame update
    void Start()
    {
        string[] list = FileManager.LoadAllLines($"{Application.dataPath}/Data/chatlog.txt");
        if(list != null)
        {
            foreach(string s in list)
            {
                AddChat(s, false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void AddChat(string text)
    {
        AddChat(text, true);
    }

    public void AddChat(string text, bool IsSave)
    {
        if (text != "")
        {
            ChatItem item = Instantiate(Resources.Load("Prefabs/UI/ChatItem") as GameObject,
                myContent).GetComponent<ChatItem>();
            Vector2 size = (item.transform as RectTransform).sizeDelta;
            size.x = (transform as RectTransform).sizeDelta.x - 21;
            (item.transform as RectTransform).sizeDelta = size;

            int dropSelect = (int)myChannel;
            text = IsSave? $"<#{chatDrops[dropSelect].Item1}>[{chatDrops[dropSelect].Item2}] {text}</color>" : text;

            item.SetText(text);

            myInput.text = string.Empty;

            //ActivateInputField: 다시 포커스를 잡아줌
            myInput.ActivateInputField();

            //스크롤을 가장 밑으로 내려줌. 하지만 Instantiate(오브젝트 생성)이 한프레임에 끝나는게 아니라서 코루틴으로 대기 후 실행
            //myScroll.value = 0;
            StartCoroutine(ScrollZero());

            if(IsSave) FileManager.AppendText($"{Application.dataPath}/Data/chatlog.txt", text);
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
