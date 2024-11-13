using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IK : MonoBehaviour
{
    public Animator myAnim;
    public Transform handSocket;
    public Transform hintSocket;
    [Range(0.0f, 1.0f)]
    public float posWeight;
    [Range(0.0f, 1.0f)]
    public float rotWeight;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnAnimatorIK(int layerIndex)
    {
        myAnim.SetIKPosition(AvatarIKGoal.LeftHand, handSocket.position);
        myAnim.SetIKPositionWeight(AvatarIKGoal.LeftHand, posWeight);
        myAnim.SetIKRotation(AvatarIKGoal.LeftHand, handSocket.rotation);
        myAnim.SetIKRotationWeight(AvatarIKGoal.LeftHand, rotWeight);
        myAnim.SetIKHintPosition(AvatarIKHint.LeftElbow, hintSocket.position);
        myAnim.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, posWeight);
    }
}
