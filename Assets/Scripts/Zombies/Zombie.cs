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
    /// 死亡生命值
    /// </summary>
    public int dieHP = 89;

    /// <summary>
    /// 僵尸移动速度
    /// </summary>
    public float speed = 0.2f;

    /// <summary>
    /// 僵尸单次伤害
    /// </summary>
    public int damage = 60;

    /// <summary>
    /// 僵尸攻击延迟
    /// </summary>
    public float attackDelay = 0.2f;
    /// <summary>
    /// 僵尸攻击间隔
    /// </summary>
    public float attackInterval = 0.6f;

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
    public string lostHeadName = "Head_LostHead";
    /// <summary>
    /// 掉头走路动画的名字
    /// </summary>
    public string lostHeadWalkName = "Zombie_LostHeadWalk";
    /// <summary>
    /// 掉头攻击动画的名字
    /// </summary>
    public string lostHeadAttackName = "Zombie_LostHeadAttack";
    /// <summary>
    /// 死亡动画的名字
    /// </summary>
    public string dieName = "Zombie_Die";
    /// <summary>
    /// 爆炸死亡动画的名字
    /// </summary>
    public string boomDieName = "Zombie_BoomDie";

    /// <summary>
    /// 身体组件
    /// </summary>
    private Transform bodyTransform;
    /// <summary>
    /// 头组件
    /// </summary>
    private Transform headTransform;
    /// <summary>
    /// 身体中心组件
    /// </summary>
    private Transform bodyCenterTransform;
    /// <summary>
    /// 身体动画组件
    /// </summary>
    private Animator bodyAnimator;
    /// <summary>
    /// 头动画组件
    /// </summary>
    private Animator headAnimator;

    /// <summary>
    /// 目标植物的Plant组件
    /// </summary>
    private Plant aimPlant;

    /// <summary>
    /// 身体的SpriteRenderer组件
    /// </summary>
    private SpriteRenderer spriteRenderer;

    /// <summary>
    /// 僵尸状态枚举
    /// </summary>
    public enum ZombieState
    {
        Walking,     // 走路
        Attacking,   // 攻击
        Dead         // 死亡
        
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
    /// 受到伤害
    /// </summary>
    /// <param name="damage">伤害值</param>
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
    /// 走路
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
    /// 吃植物
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
    /// 死亡
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
    /// 失去脑袋走路
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
