using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimEvent : MonoBehaviour
{
    public UnityEvent attackAction;
    public UnityEvent jumpAttackAction;
    public UnityEvent deadAction;
    public void OnAttack()
    {
        attackAction?.Invoke();
    }
    public void OnJumpAttack()
    {
        jumpAttackAction?.Invoke();
    }
    public void OnDead()
    {
        deadAction?.Invoke();
    }
}
