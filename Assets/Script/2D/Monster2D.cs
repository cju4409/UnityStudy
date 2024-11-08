using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Monster2D : BattleSystem2D
{
    Transform myTarget;
    public enum State
    {
        Create, Normal, Battle, Dead
    }
    State myState = State.Create;
    float maxDist = 0.0f;
    Transform curGround;


    void ChangeState(State s)
    {
        if (myState == s) return;
        myState = s;
        switch (s)
        {
            case State.Normal:
                if (Random.Range(0, 2) == 0)
                {
                    moveDir.x = 1.0f;
                }
                else
                {
                    moveDir.x = -1.0f;
                }
                break;
            case State.Battle:
                break;
            case State.Dead:
                myRigid.isKinematic = true;
                myColider.isTrigger = true;
                gameObject.layer = 0;
                moveDir.x = 0.0f;
                break;
        }
    }

    void StateProcess()
    {
        switch (myState)
        {
            case State.Normal:
                maxDist -= deltaDist;
                if (!myAnim.GetBool("IsAir") && maxDist <= 0.0f)
                {
                    moveDir *= -1.0f;
                    myRenderer.flipX = !myRenderer.flipX;
                    if (curGround != null) OnCheckGround(curGround);
                }
                break;
            case State.Battle:
                moveDir.x = myTarget.position.x > transform.position.x ? 1.0f : myTarget.position.x < transform.position.x ? -1.0f : 0.0f;

                playTime += Time.deltaTime;
                if (Vector2.Distance(myTarget.position, transform.position) <= battleStat.AttackRange)
                {
                    moveDir.x = 0.0f;
                    if (playTime >= battleStat.AttackDelay)
                    {
                        myAnim.SetTrigger(animData.OnAttack);
                        playTime = 0.0f;
                    }
                }
                break;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        ChangeState(State.Normal);
    }

    // Update is called once per frame
    void Update()
    {
        OnUpdate();
        StateProcess();
    }

    protected override void OnCheckGround(Transform tr)
    {
        curGround = tr;
        float halfDist = tr.localScale.x * 0.5f;
        float dist = tr.position.x - transform.position.x;
        if (myRenderer.flipX)
        {
            maxDist = halfDist - dist;
        }
        else
        {
            maxDist = halfDist + dist;
        }
    }

    public void OnFindTarget(Transform tr)
    {
        myTarget = tr;
        ChangeState(State.Battle);
    }
    public void OnLostTarget()
    {
        myTarget = null;
        if (myState != State.Dead) ChangeState(State.Normal);
    }
    public void OnAttack()
    {
        myTarget.GetComponent<IDamage>()?.OnDamage(battleStat.AP);
    }
    protected override void OnDead()
    {
        base.OnDead();
        ChangeState(State.Dead);
        StopAllCoroutines();
        StartCoroutine(Dead());
    }

    IEnumerator Dead()
    {
        float dist = 2.0f;

        yield return new WaitForSeconds(3.0f);
        while (dist > 0.0f)
        {
            UnityEngine.Color color = myRenderer.color;
            color.a -= Time.deltaTime * 0.5f;
            myRenderer.color = color;
            transform.Translate(Vector3.up * Time.deltaTime);
            dist -= Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }

}
