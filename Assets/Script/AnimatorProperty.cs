using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AnimParameterData
{
    public int IsMoving;
    public int OnAttack;
    public int OnJumpAttack;
    public int IsAttack;
    public int OnDamage;
    public int OnDead;
    public int OnJump;
    public int OnRoll;
    public int IsRoll;
    public int IsCombable;

    public void Initialize()
    {
        IsMoving = Animator.StringToHash("IsMoving");
        OnAttack = Animator.StringToHash("OnAttack");
        OnJumpAttack = Animator.StringToHash("OnJumpAttack");
        IsAttack = Animator.StringToHash("IsAttack");
        OnDamage = Animator.StringToHash("OnDamage");
        OnDead = Animator.StringToHash("OnDead");
        OnJump = Animator.StringToHash("OnJump");
        OnRoll = Animator.StringToHash("OnRoll");
        IsRoll = Animator.StringToHash("IsRoll");
        IsCombable = Animator.StringToHash("IsCombable");
    }
}

public class AnimatorProperty : MonoBehaviour
{
    Animator _anim = null;
    protected AnimParameterData animData = new AnimParameterData();
    protected Animator myAnim
    {
        get
        {   
            if(_anim == null)
            {
                _anim = GetComponent<Animator>();
                if(_anim == null)
                {
                    _anim = GetComponentInChildren<Animator>();
                }
                animData.Initialize();
            }
            return _anim;
        }
    }
}
