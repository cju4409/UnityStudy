using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PopupManager : MonoBehaviour
{
    public GameObject myNoTouch;
    public static PopupManager Instance
    {
        get; private set;
    }

    Stack<Popup> closeList = new Stack<Popup>();

    private void Awake()
    {
        Instance = this;
    }

    public void AddPopup(string title, string content)
    {
        myNoTouch.SetActive(true);
        //SetAsFirstSibling: 같은 계층에서 가장 처음으로 내림
        //SetAsLastSibling: 같은 계층에서 가장 마지막으로 내림
        //SetSiblingIndex: 같은 계층에서 인덱스에 해당하는 위치로 이동
        myNoTouch.transform.SetAsLastSibling();

        Popup pop = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Popup"), transform).GetComponent<Popup>();
        pop.myTitle.text = title;
        pop.myContent.text = content;
        pop.closeAlarm += () =>
        {
            if (transform.childCount > 2)
            {
                myNoTouch.transform.SetSiblingIndex(transform.childCount - 3);
            }
            else
            {
                myNoTouch.SetActive(false);
            }
        };

        closeList.Push(pop);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            AddPopup("테스트", "테스트 입니다!!");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            while (closeList.Count > 0)
            {
                Popup pop = closeList.Pop();
                if (pop != null)
                {
                    pop.OnClose();
                    break;
                }
            }
        }
    }
}
