using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIcon : DragItem
{
    public ItemData myData;

    public void SetData(ItemData data)
    {
        myData = data;
        myImage.sprite = myData.icon;
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
