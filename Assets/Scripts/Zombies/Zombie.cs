using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 僵尸
/// </summary>
public class Zombie : MonoBehaviour
{
    /// <summary>
    /// 生命值
    /// </summary>
    public int HP = 270;

    /// <summary>
    /// 僵尸移动速度
    /// </summary>
    public float speed = 0.24f;

    /// <summary>
    /// 僵尸单次伤害
    /// </summary>
    public int damage = 40;

    /// <summary>
    /// 僵尸攻击间隔
    /// </summary>
    public float attackInterval = 0.4f;

    /// <summary>
    /// 走路动画的名字
    /// </summary>
    public string walkName = "Zombie_Walk1";
    /// <summary>
    /// 攻击动画的名字
    /// </summary>
    public string attackName = "Zombie_Attack";
    /// <summary>
    /// 掉头动画的名字
    /// </summary>
    public string lostHeadName = "Zombie_LostHead";
    /// <summary>
    /// 死亡动画的名字
    /// </summary>
    public string dieName = "Zombie_Die";
    /// <summary>
    /// 爆炸死亡动画的名字
    /// </summary>
    public string boomDieName = "Zombie_BoomDie";

    /// <summary>
    /// 动画组件
    /// </summary>
    private Animator animator;

    /// <summary>
    /// 目标植物的Plant组件
    /// </summary>
    private Plant aimPlant;

    /// <summary>
    /// 僵尸状态枚举
    /// </summary>
    public enum ZombieState
    {
        Walking,
        Attacking,
        Dead
    }

    /// <summary>
    /// 僵尸状态
    /// </summary>
    private ZombieState state;

    /// <summary>
    /// 僵尸状态
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
    /// 受到伤害
    /// </summary>
    /// <param name="damage">伤害值</param>
    private void GetHurt(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            State = ZombieState.Dead;
        }
    }

    /// <summary>
    /// 走路
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
    /// 吃植物
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
    /// 死亡
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
