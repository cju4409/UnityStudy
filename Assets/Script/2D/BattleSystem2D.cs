using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BattleSystem2D : Movement2D, IDamage
{
    public BattleStat battleStat;
    protected float playTime = 0.0f;

    public void OnDamage(float dmg)
    {
        battleStat.CurHP -= dmg;
        if(battleStat.CurHP > 0.0f)
        {
            myAnim.SetTrigger(animData.OnDamage);
        } else
        {
            OnDead();
        }
    }

    protected virtual void OnDead()
    {
        myAnim.ResetTrigger(animData.OnAttack);
        myAnim.SetTrigger(animData.OnDead);
    }
}
