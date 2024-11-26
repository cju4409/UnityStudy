using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// where T : class  => T는 무조건 class 타입이라는 것을 정의해줌으로써 클래스의 기능이 사용가능해짐
// where T : Object  => T는 무조건 Object 타입이라는 것을 정의해줌으로써 Object의 기능이 사용가능해짐
public class Singleton<T> : MonoBehaviour where T : Component
{
    static T _Inst = null;
    public static T Instance
    {
        get
        {
            if (_Inst == null)
            {
                _Inst = FindObjectOfType<T>();
                if (_Inst == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).ToString();
                    _Inst = obj.AddComponent<T>();
                }
            }
            return _Inst;
        }
    }

    protected void Initialize()
    {
        if (_Inst != null && _Inst != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
