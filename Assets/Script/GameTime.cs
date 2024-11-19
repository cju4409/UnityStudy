using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameTime
{
    static Dictionary<int, WaitForSeconds> waitList = new Dictionary<int, WaitForSeconds>(); 

    public static WaitForSeconds Wait(float t)
    {
        int n = (int)(t * 100.0f);
        if (!waitList.ContainsKey(n)) waitList.Add(n, new WaitForSeconds(t));
        return waitList[n];
    }
}
