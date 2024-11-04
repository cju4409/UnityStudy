using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public Transform myTarget;
    public Slider mySlider;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (myTarget != null)
        {
            transform.position = Camera.main.WorldToScreenPoint(myTarget.position);
            if(transform.position.z < 0.0f)
            {
                transform.position = new Vector3(0, 10000, 0);
            }
        }
    }

    public void OnChange(float v)
    {
        mySlider.value = v;
    }
}
