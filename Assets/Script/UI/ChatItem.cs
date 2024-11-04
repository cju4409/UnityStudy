using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatItem : MonoBehaviour
{
    public TMPro.TMP_Text myText;
    public void SetText(string txt)
    {
        myText.text = txt;
        Vector2 size = (transform as RectTransform).sizeDelta;

        //GetPreferredValues: 입력된 내용의 크기가 얼마인지 알려줌
        size.y = myText.GetPreferredValues().y + 4;

        (transform as RectTransform).sizeDelta = size;

    }
}
