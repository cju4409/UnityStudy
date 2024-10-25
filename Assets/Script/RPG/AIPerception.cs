using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Events;

public class AIPerception : MonoBehaviour
{
    public LayerMask mask;
    public GameObject target;
    public UnityEvent<GameObject> findAction;
    public UnityEvent lostAction;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((1 << other.gameObject.layer & mask) != 0)
        {
            target = other.gameObject;
            findAction?.Invoke(target);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if(target == other.gameObject)
        {
            target = null;
            lostAction?.Invoke();
        }
    }
}
