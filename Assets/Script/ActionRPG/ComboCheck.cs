using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboCheck : AnimatorProperty
{
    bool IsChecking = false;
    int clickCount = 0;
    public void CheckStart()
    {
        StartCoroutine(Checking());
    }

    public void CheckEnd()
    {
        IsChecking = false;
    }

    public void OnResult()
    {
        if(clickCount == 0)
        {
            myAnim.SetBool(animData.IsAttack, false);
        }
        else
        {
            myAnim.SetBool(animData.IsCombable, true);
        }
    }

    IEnumerator Checking()
    {
        clickCount = 0;
        IsChecking = true;
        myAnim.SetBool(animData.IsCombable, false);
        while (IsChecking)
        {
            if (Input.GetMouseButtonDown(0))
            {
                clickCount++;
            }
            yield return null;
        }
    }
}
