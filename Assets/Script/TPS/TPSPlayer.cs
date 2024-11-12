using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSPlayer : AnimatorProperty
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        myAnim.SetFloat("x", Input.GetAxis("Horizontal"));
        myAnim.SetFloat("y", Input.GetAxis("Vertical"));
    }
}
