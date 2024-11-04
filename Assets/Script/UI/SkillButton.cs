using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class SkillButton : ImageProperty, IPointerClickHandler
{
    public UnityEvent skillAction;
    public float coolTime = 1.0f;
    // IPointerClickHandler.OnPointerClick ���콺 Ŭ���� �Ǹ� �ߵ���
    public void OnPointerClick(PointerEventData eventData)
    {
        //clickCount : ���������� ��� Ŭ���ߴ����� ���� ����
        if(eventData.clickCount == 2 && myImage.fillAmount >= 1.0f)
        {
            StartCoroutine(Cooling());
        }
    }

    IEnumerator Cooling()
    {
        skillAction?.Invoke();
        float speed = 1.0f / coolTime;
        myImage.fillAmount = 0.0f;
        while (myImage.fillAmount < 1.0f)
        {
            myImage.fillAmount += Time.deltaTime * speed;
            yield return null;
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
