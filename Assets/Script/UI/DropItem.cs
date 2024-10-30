using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// IDropHandler: �巡���ϰ��� ��������� ����
public class DropItem : MonoBehaviour, IDropHandler
{
    DragItem myChild = null;
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag.name);
        // eventData.pointerDrag : ����� ���� �ִ� ������Ʈ
        DragItem newChild = eventData.pointerDrag.GetComponent<DragItem>();
        if (newChild == null) return;
        Transform p = newChild.myParent;
        newChild.OnChangeParent(transform);
        if (myChild != null)
        {
            myChild.OnChangeParent(p, true);
        }
        //OnChangeChild(newChild);
        myChild = newChild;
    }

    public void OnChangeChild(DragItem child)
    {
        myChild = child;
    }

    // Start is called before the first frame update
    void Start()
    {
        myChild = GetComponentInChildren<DragItem>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
