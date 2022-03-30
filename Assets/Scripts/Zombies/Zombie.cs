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
    /// ��ʬ�ƶ��ٶ�
    /// </summary>
    public float speed = 0.24f;

    /// <summary>
    /// ��ʬ�����˺�
    /// </summary>
    public int damage = 40;

    /// <summary>
    /// ��ʬ�������
    /// </summary>
    public float attackInterval = 0.4f;

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
    public string lostHeadName = "Zombie_LostHead";
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
    private Animator animator;

    /// <summary>
    /// Ŀ��ֲ���Plant���
    /// </summary>
    private Plant aimPlant;

    /// <summary>
    /// ��ʬ״̬ö��
    /// </summary>
    public enum ZombieState
    {
        Walking,
        Attacking,
        Dead
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
            state = value;
            switch(state)
            {
                case ZombieState.Walking:
                    StartCoroutine(Walk());
                    break;
                case ZombieState.Attacking:
                    StartCoroutine(Attack());
                    break;
                case ZombieState.Dead:
                    StartCoroutine(Die());
                    break;
            }
        }
    }

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        State = ZombieState.Walking;
    }

    /// <summary>
    /// �ܵ��˺�
    /// </summary>
    /// <param name="damage">�˺�ֵ</param>
    private void GetHurt(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            State = ZombieState.Dead;
        }
    }

    /// <summary>
    /// ��·
    /// </summary>
    /// <returns></returns>
    private IEnumerator Walk()
    {
        animator.Play(walkName);
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
        animator.Play(attackName);
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
    private IEnumerator Die()
    {
        animator.Play(dieName);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (State == ZombieState.Dead) return;
        if (collision.tag == "Pea")
        {
            GetHurt(collision.GetComponent<Pea>().damage);
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "Plant")
        {
            aimPlant = collision.GetComponent<Plant>();
            State = ZombieState.Attacking;
        }
    }
}
