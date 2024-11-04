using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Resize : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Vector2 minSize = new Vector2(200, 100);
    Vector2 maxSize = new Vector2(600, 600);
    public RectTransform myRect;
    Vector2 orgSize;
    Vector2 startPos;

    public void OnBeginDrag(PointerEventData eventData)
    {
        orgSize = myRect.sizeDelta;
        startPos = eventData.position;
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 deltaSize = eventData.position - startPos;
        Vector2 curSize = orgSize;
        curSize.x = Mathf.Clamp(curSize.x + deltaSize.x, minSize.x, maxSize.x);
        curSize.y = Mathf.Clamp(curSize.y + deltaSize.y, minSize.y, maxSize.y);
        myRect.sizeDelta = curSize;
        myRect.anchoredPosition = myRect.sizeDelta * 0.5f;
    }

    public void OnEndDrag(PointerEventData eventData)
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
