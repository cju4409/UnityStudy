using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSPlayer : AnimatorProperty
{
    public Transform mySpin;
    public Transform mySpringArm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        myAnim.SetFloat("x", Input.GetAxis("Horizontal"));
        myAnim.SetFloat("y", Input.GetAxis("Vertical"));

        if (Input.GetMouseButtonDown(0)) myAnim.SetBool("IsFire", true);
        if (Input.GetMouseButtonUp(0)) myAnim.SetBool("IsFire", false);
    }

    private void LateUpdate()
    {
        mySpin.localRotation = mySpringArm.localRotation;
    }
}
