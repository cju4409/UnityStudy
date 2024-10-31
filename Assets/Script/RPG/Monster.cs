using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Monster : AIMovement
{
    public enum State
    {
        Create, Normal, Roaming, Battle, Death
    }
    public State myState = State.Create;
    Vector3 createPos;

    Coroutine myCoro;
    public Transform barPoint;

    void ChangeState(State s)
    {
        if (myState == s) return;
        myState = s;
        switch (myState)
        {
            case State.Normal:
                {
                    OnStop();
                    Vector3 dir = Vector3.forward;
                    float angle = Random.Range(0f, 360.0f);
                    //����ȸ�� => ���� * ���ʹϾ�(�����), ���͸� ȸ����Ű�� �ʹٸ� ���ʹϾ�(�����)�� ���ؾ���
                    Quaternion rot = Quaternion.Euler(0, angle, 0);
                    dir = rot * dir;
                    float dist = Random.Range(0.5f, 5.0f);
                    dir = dir * dist;
                    Vector3 randomPos = createPos + dir;
                    OnMove(randomPos, () => myCoro = StartCoroutine(DelayAction(Random.Range(1.0f, 3.0f), () => ChangeState(State.Normal))));
                    ChangeState(State.Roaming);
                }
                break;
            case State.Battle:
                OnStop();
                base.OnFollow(myTarget.transform);
                break;
            case State.Death:
                StopAllCoroutines();
                break;
        }
    }

    void StateProcess()
    {
        switch (myState)
        {
            case State.Normal:
                break;
            case State.Battle:
                playTime += Time.deltaTime;
                break;
        }
    }

    public void OnBattle(GameObject target)
    {
        if (myState == State.Death) return;
        myTarget = target;
        ChangeState(State.Battle);
    }

    //�Լ��� newŰ���带 �ٿ��ָ� �θ𿡼� virtualŰ���带 ������ �ʰ� ������ ������
    private new void OnStop()
    {
        if (myCoro != null) StopCoroutine(myCoro);
        base.OnStop();
        myCoro = null;
    }

    public void OnNormal()
    {
        if (myState == State.Death) return;
        myTarget = null;
        ChangeState(State.Normal);
    }

    protected override void OnDead()
    {
        base.OnDead();
        ChangeState(State.Death);
    }

    public void OnDelete()
    {
        StartCoroutine(DeleteAction(3));
    }

    //yield return�� �Ϲ��Լ������� �� �� ������ ���� �ڷ�ƾ������ ��밡��
    IEnumerator DelayAction(float t, UnityEngine.Events.UnityAction act)
    {
        yield return new WaitForSeconds(t);
        act?.Invoke();
    }
    IEnumerator DeleteAction(float t)
    {
        yield return new WaitForSeconds(t);
        Vector3 dir = Vector3.down;
        float dist = 3.0f;
        while (dist > 0.0f)
        {
            float delta = 1.0f * Time.deltaTime;
            transform.Translate(dir * delta);
            dist -= delta;

            yield return null;
        }
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        OnReset();
        createPos = transform.position;
        ChangeState(State.Normal);

        //Resources�����ȿ� �ִ°͵��� Resources Ŭ������ ���ؼ� �ҷ��� �� ����
        HpBar hpBar = Instantiate(Resources.Load("Prefabs/HpBar") as GameObject,
            SceneData.Instance.hpBarRoot).GetComponent<HpBar>();
        hpBar.myTarget = barPoint;

        //AddListener: �ڵ�� UnityEvent ��������Ʈ �߰�
        hpObserbs.AddListener(hpBar.OnChange);

        deathAlarm += () => Destroy(hpBar.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        StateProcess();
    }
}
