using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// IDropHandler: 드래그하고나서 드롭했을때 실행
public class DropItem : MonoBehaviour, IDropHandler
{
    DragItem myChild = null;
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag.name);
        // eventData.pointerDrag : 드랍한 곳에 있는 오브젝트
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
