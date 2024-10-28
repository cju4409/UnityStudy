using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//인터페이스(interface) : 추상함수만 가지고 있는 추상클래스, 다중 상속 가능, 프로퍼티도 함수라서 가능
//함수는 모두 public으로 지정해줘야함
interface IDeathAlarm
{
    UnityAction deathAlarm { get; set; }
}
interface ILive
{
    bool IsLive { get; }
}
interface IDamage
{
    public void OnDamage(float dmg);
}
interface IBattle : IDeathAlarm, ILive, IDamage { }

//직렬화하는 코드:[System.Serializable] 직렬화 하지 않으면 public struct(구조체)가 유니티 에디터 inspector에서 보이지 않음
[System.Serializable]

public struct BattleStat
{
    public float AP;
    public float AttackRange;
    public float AttackDelay;
    public float MaxHP;
    public float CurHP;
}
public class BattleSystem : AnimatorProperty, IBattle
{
    public BattleStat battleStat;
    protected float playTime = 0.0f;
    public GameObject myTarget;
    //인스펙트에서 확인할 수 없고 실시간 연동되므로 UnityEvent가 아닌 UnityAction 사용
    public UnityAction deathAlarm { get; set; }
    public UnityEvent<float> hpObserbs;
    public bool IsLive
    {
        get
        {
            return battleStat.CurHP > 0.0f;
        }
    }

    public float curHP
    {
        get => battleStat.CurHP;
        set
        {

            battleStat.CurHP = value;
            hpObserbs?.Invoke(battleStat.CurHP/battleStat.MaxHP);
        }
    }


    protected void OnReset()
    {
        battleStat.CurHP = battleStat.MaxHP;
    }

    protected virtual void OnDead()
    {
        deathAlarm?.Invoke();
    }

    public void OnDamage(float dmg)
    {
        curHP -= dmg;
        if (curHP > 0.0f)
        {
            myAnim.SetTrigger(animData.OnDamage);
        }
        else
        {
            OnDead();
            myAnim.SetTrigger(animData.OnDead);
        }
    }

    public void OnAttack()
    {
        myTarget?.GetComponent<IDamage>().OnDamage(battleStat.AP);
    }
}
