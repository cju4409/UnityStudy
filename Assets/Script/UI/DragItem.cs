using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

//IBeginDragHandler : 드래그 시작,IDragHandler : 드래그 중, IEndDragHandler : 드래그 끝
public class DragItem : ImageProperty, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Vector2 dragOffset = Vector3.zero;
    public Transform myParent
    {
        get; private set;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        myParent = transform.parent;
        transform.SetParent(transform.parent.parent.parent);
        dragOffset = (Vector2)transform.position - eventData.position;
        myImage.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position + dragOffset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(myParent);
        myImage.raycastTarget = true;
        transform.localPosition = Vector3.zero;
    }


    public void OnChangeParent(Transform p, bool change = false)
    {
        if (!change)
        {
            myParent.GetComponent<DropItem>().OnChangeChild(null);
            myParent = p;
        } else
        {
            myParent = p;
            transform.SetParent(myParent);
            transform.localPosition = Vector3.zero;
            myParent.GetComponent<DropItem>().OnChangeChild(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
