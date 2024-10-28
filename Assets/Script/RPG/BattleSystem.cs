using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//�������̽�(interface) : �߻��Լ��� ������ �ִ� �߻�Ŭ����, ���� ��� ����, ������Ƽ�� �Լ��� ����
//�Լ��� ��� public���� �����������
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

//����ȭ�ϴ� �ڵ�:[System.Serializable] ����ȭ ���� ������ public struct(����ü)�� ����Ƽ ������ inspector���� ������ ����
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
    //�ν���Ʈ���� Ȯ���� �� ���� �ǽð� �����ǹǷ� UnityEvent�� �ƴ� UnityAction ���
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
