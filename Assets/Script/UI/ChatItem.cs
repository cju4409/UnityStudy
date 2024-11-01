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

        //GetPreferredValues: �Էµ� ������ ũ�Ⱑ ������ �˷���
        size.y = myText.GetPreferredValues().y + 4;

        (transform as RectTransform).sizeDelta = size;

    }
}
