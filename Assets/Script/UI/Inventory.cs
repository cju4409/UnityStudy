using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 스택: 지역변수 (CPU가 자동 관리, 읽기 쓰기 속도 가장 빠름, 블록이 닫히면 제거)
// 힙: 인스턴스 (동적데이터, 가비지(참조카운트가 0이면 가비지)컬렉터가 관리)
// 데이터: static, 상수 (생성 : 프로그램이 시작, 제거 : 프로그램이 종료)
// 코드: 함수
public class Inventory : MonoBehaviour
{
    // static 멤버변수 클래스: 그냥 클래스의 인스턴스를 참조하는 변수 (메모리 주소값을 저장할뿐)
    public static Inventory Instance
    {
        get; private set;
    }
    public ItemSlot[] mySlots;
    public ItemData[] itemDataList;

    ItemSlot FindBlankSlot()
    {
        foreach(ItemSlot s in mySlots)
        {
            if(s.myChild == null)
            {
                return s;
            }
        }
        return null;
    }

    void AddItem(int i)
    {
        ItemSlot slot = FindBlankSlot();
        if ( slot != null )
        {
            slot.myChild = Instantiate(Resources.Load<GameObject>("Prefabs/ItemIcon"), slot.transform).GetComponent<DragItem>();
            (slot.myChild as ItemIcon).SetData(itemDataList[i]);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        gameObject.SetActive(false);
        mySlots = GetComponentsInChildren<ItemSlot>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F8)) {
            AddItem(Random.Range(0, itemDataList.Length));
        }
    }
}
