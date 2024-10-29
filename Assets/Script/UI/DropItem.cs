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
        // eventData.pointerDrag : ����� ���� �ִ� ������Ʈ
        DragItem newChild = eventData.pointerDrag.GetComponent<DragItem>();
        if (newChild == null) return;
        if (myChild != null)
        {
            myChild.OnChangeParent(newChild.myParent, true);
        }
        newChild.OnChangeParent(transform);
        OnChangeChild(newChild);
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
