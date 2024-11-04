using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PopupTitle : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    Vector2 dragOffset = Vector2.zero;
    Rect rootSize = Rect.zero;
    public void OnBeginDrag(PointerEventData eventData)
    {
        rootSize = (transform.parent as RectTransform).rect;
        dragOffset = (Vector2)transform.parent.position - eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos = dragOffset + eventData.position;
        pos.x = Mathf.Clamp(pos.x, rootSize.width * 0.5f, Screen.width - rootSize.width * 0.5f);
        pos.y = Mathf.Clamp(pos.y, rootSize.height * 0.5f, Screen.height - rootSize.height * 0.5f);
        transform.parent.position = pos;
    }

    public  void OnEndDrag(PointerEventData eventData)
    {
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
