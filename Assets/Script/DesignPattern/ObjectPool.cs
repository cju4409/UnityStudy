using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{
    Queue<GameObject> pool = new Queue<GameObject>();

    private void Awake()
    {
        base.Initialize();
    }

    public GameObject GetObject(GameObject org, Transform parent)
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
            obj.transform.parent = parent;
            obj.transform.localPosition = Vector3.zero;
            obj.transform.rotation = Quaternion.identity;
            return obj;
        }
        return Instantiate(org, parent);
    }

    public void Release(GameObject obj)
    {
        obj.transform.SetParent(transform);
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}
