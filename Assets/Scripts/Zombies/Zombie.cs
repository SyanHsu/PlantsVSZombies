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
    /// ����ֵ
    /// </summary>
    public int HP = 270;

    /// <summary>
    /// ��������ֵ
    /// </summary>
    public int dieHP = 89;

    /// <summary>
    /// ��ʬ�ƶ��ٶ�
    /// </summary>
    public float speed = 0.2f;

    /// <summary>
    /// ��ʬ�����˺�
    /// </summary>
    public int damage = 60;

    /// <summary>
    /// ��ʬ�����ӳ�
    /// </summary>
    public float attackDelay = 0.2f;
    /// <summary>
    /// ��ʬ�������
    /// </summary>
    public float attackInterval = 0.6f;

    /// <summary>
    /// ��·����������
    /// </summary>
    public string walkName = "Zombie_Walk1";
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
    private Transform bodyCenterTransform;
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

    private void Start()
    {
        bodyTransform = transform.Find("Body");
        headTransform = transform.Find("Head");
        bodyCenterTransform = bodyTransform.Find("Center");
        bodyAnimator = bodyTransform.GetComponent<Animator>();
        headAnimator = headTransform.GetComponent<Animator>();
        spriteRenderer = bodyTransform.GetComponent<SpriteRenderer>();
        State = ZombieState.Walking;
    }

    /// <summary>
    /// �ܵ��˺�
    /// </summary>
    /// <param name="damage">�˺�ֵ</param>
    private void GetHurt(int damage)
    {
        HP -= damage;
        StartCoroutine(Shine());
        if (HP <= dieHP)
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
        bodyAnimator.CrossFadeInFixedTime(walkName, attackDelay);
        while (State == ZombieState.Walking)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);

            yield return 0;
        }
    }

    /// <summary>
    /// ��ֲ��
    /// </summary>
    /// <returns></returns>
    private IEnumerator Attack()
    {
        bodyAnimator.CrossFadeInFixedTime(attackName, attackDelay);
        yield return new WaitForSeconds(attackDelay);
        while (State == ZombieState.Attacking)
        {
            aimPlant.GetHurt(damage);
            if (aimPlant.HP <= 0)
            {
                State = ZombieState.Walking;
            }
            else
            {
                yield return new WaitForSeconds(attackInterval);
            }
        }
    }

    /// <summary>
    /// ����
    /// </summary>
    /// <returns></returns>
    private IEnumerator Die(int dieMode)
    {
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
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    /// <summary>
    /// ʧȥ�Դ���·
    /// </summary>
    /// <returns></returns>
    private IEnumerator LostHeadWalk()
    {
        float timer = 0;
        while (timer < 1f)
        {
            bodyTransform.Translate(Vector3.left * speed * Time.deltaTime);
            timer += Time.deltaTime;
            yield return 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            GetHurt(collision.GetComponent<Pea>().damage);
            collision.GetComponent<Bullet>().IsHit = true;
            collision.transform.position = bodyCenterTransform.position;
        }
        else if (collision.tag == "Plant")
        {
            aimPlant = collision.GetComponent<Plant>();
            State = ZombieState.Attacking;
        }
    }
}
