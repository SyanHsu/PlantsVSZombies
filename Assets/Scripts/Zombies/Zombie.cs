using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ʬ
/// </summary>
public class Zombie : MonoBehaviour
{
    /// <summary>
    /// ��ʬ��Ϣ
    /// </summary>
    public ZombieInfo zombieInfo;

    /// <summary>
    /// ��ǰ����ֵ
    /// </summary>
    public int currentHP;

    /// <summary>
    /// ���ڵ�����
    /// </summary>
    public int row;

    /// <summary>
    /// ��·����������
    /// </summary>
    public string walkName;
    /// <summary>
    /// ��������������
    /// </summary>
    public string attackName = "Zombie_Attack";
    /// <summary>
    /// ��ͷ����������
    /// </summary>
    public string lostHeadName = "Head_LostHead";
    /// <summary>
    /// ��ͷ��·����������
    /// </summary>
    public string lostHeadWalkName = "Zombie_LostHeadWalk";
    /// <summary>
    /// ��ͷ��������������
    /// </summary>
    public string lostHeadAttackName = "Zombie_LostHeadAttack";
    /// <summary>
    /// ��������������
    /// </summary>
    public string dieName = "Zombie_Die";
    /// <summary>
    /// ��ը��������������
    /// </summary>
    public string boomDieName = "Zombie_BoomDie";

    /// <summary>
    /// �������
    /// </summary>
    private Transform bodyTransform;
    /// <summary>
    /// ͷ���
    /// </summary>
    private Transform headTransform;
    /// <summary>
    /// �����������
    /// </summary>
    public Transform bodyCenterTransform;
    /// <summary>
    /// ���嶯�����
    /// </summary>
    private Animator bodyAnimator;
    /// <summary>
    /// ͷ�������
    /// </summary>
    private Animator headAnimator;

    /// <summary>
    /// Ŀ��ֲ���Plant���
    /// </summary>
    private Plant aimPlant;

    /// <summary>
    /// �����SpriteRenderer���
    /// </summary>
    private SpriteRenderer spriteRenderer;

    /// <summary>
    /// ��ʬ״̬ö��
    /// </summary>
    public enum ZombieState
    {
        Walking,     // ��·
        Attacking,   // ����
        Dead         // ����
    }

    /// <summary>
    /// ��ʬ״̬
    /// </summary>
    private ZombieState state;

    /// <summary>
    /// ��ʬ״̬
    /// </summary>
    private ZombieState State
    {
        get => state;
        set
        {
            if (value == ZombieState.Dead && state != ZombieState.Dead)
            {
                StartCoroutine(Die((int)state));
            }
            state = value;
            switch(state)
            {
                case ZombieState.Walking:
                    StartCoroutine(Walk());
                    break;
                case ZombieState.Attacking:
                    StartCoroutine(Attack());
                    break;
            }
        }
    }

    private void Awake()
    {
        bodyTransform = transform.Find("Body");
        headTransform = transform.Find("Head");
        bodyCenterTransform = bodyTransform.Find("Center");
        bodyAnimator = bodyTransform.GetComponent<Animator>();
        headAnimator = headTransform.GetComponent<Animator>();
        spriteRenderer = bodyTransform.GetComponent<SpriteRenderer>();
    }

    public void Init(ZombieInfo zombieInfo, int row, int sortingOrder, int walkIndex)
    {
        GetComponent<BoxCollider2D>().enabled = true;
        this.zombieInfo = zombieInfo;
        currentHP = zombieInfo.HP;
        this.row = row;
        bodyTransform.localPosition = new Vector3(0, 0.47f);
        headTransform.GetComponent<SpriteRenderer>().sortingOrder = 
            spriteRenderer.sortingOrder = sortingOrder;
        walkName = "Zombie_Walk" + walkIndex;
        State = ZombieState.Walking;
    }

    /// <summary>
    /// �ܵ��˺�
    /// </summary>
    /// <param name="damage">�˺�ֵ</param>
    public void GetHurt(int damage)
    {
        currentHP -= damage;
        StartCoroutine(Shine());
        if (currentHP <= zombieInfo.dieHP)
        {
            State = ZombieState.Dead;
        }
    }

    private IEnumerator Shine()
    {
        spriteRenderer.material.color = new Color(0.8f, 0.8f, 0.8f, 1);
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.material.color = Color.white;
    }

    /// <summary>
    /// ��·
    /// </summary>
    /// <returns></returns>
    private IEnumerator Walk()
    {
        bodyAnimator.CrossFadeInFixedTime(walkName, 0.2f);
        while (State == ZombieState.Walking)
        {
            transform.Translate(Vector3.left * zombieInfo.speed * Time.deltaTime);

            yield return 0;
        }
    }

    /// <summary>
    /// ��ֲ��
    /// </summary>
    /// <returns></returns>
    private IEnumerator Attack()
    {
        bodyAnimator.CrossFadeInFixedTime(attackName, 0.2f);
        yield return new WaitForSeconds(0.2f);
        while (State == ZombieState.Attacking)
        {
            aimPlant.GetHurt(zombieInfo.damage);
            if (aimPlant.currentHP <= 0)
            {
                State = ZombieState.Walking;
            }
            else
            {
                yield return new WaitForSeconds(zombieInfo.attackInterval);
            }
        }
    }

    /// <summary>
    /// ����
    /// </summary>
    /// <returns></returns>
    private IEnumerator Die(int dieMode)
    {
        ZombieManager.Instance.RemoveZombie(this);
        switch (dieMode)
        {
            case 0:
                bodyAnimator.Play(lostHeadWalkName, 0, bodyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime);
                StartCoroutine(LostHeadWalk());
                break;
            case 1:
                bodyAnimator.Play(lostHeadAttackName, 0, bodyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime);
                break;
        }
        headAnimator.Play(lostHeadName);
        yield return new WaitForSeconds(2f);
        GetComponent<BoxCollider2D>().enabled = false;
        headAnimator.Play("Default");
        bodyAnimator.Play(dieName);
        yield return new WaitForSeconds(2f);
        SelfDestroy();
    }

    /// <summary>
    /// ʧȥ�Դ���·
    /// </summary>
    /// <returns></returns>
    private IEnumerator LostHeadWalk()
    {
        float timer = 0;
        while (timer < 2f)
        {
            bodyTransform.Translate(Vector3.left * zombieInfo.speed * Time.deltaTime);
            timer += Time.deltaTime;
            yield return 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Plant")
        {
            aimPlant = collision.GetComponent<Plant>();
            State = ZombieState.Attacking;
        }
    }

    private void SelfDestroy()
    {
        StopAllCoroutines();
        PoolManager.Instance.PushGameObject(gameObject, zombieInfo.prefab);
    }    
}