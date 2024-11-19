using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Movement
{
    // Start is called before the first frame update
    void Start()
    {
        OnReset();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBattle(GameObject target)
    {
        if (target.GetComponent<ILive>().IsLive)
        {
            myTarget = target;
            myTarget.GetComponent<IDeathAlarm>().deathAlarm = () =>
            {
                Debug.Log("데스알람!");
                StopAllCoroutines();
                myTarget = null;
            };
            if (target != null) base.OnFollow(target.transform);
        }
    }

    public void GotoMenu()
    {
        Loading.LoadScene(0);
    }
}
