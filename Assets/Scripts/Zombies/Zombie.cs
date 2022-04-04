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
    /// 僵尸信息
    /// </summary>
    public ZombieInfo zombieInfo;

    /// <summary>
    /// 当前生命值
    /// </summary>
    public int currentHP;

    /// <summary>
    /// 处于的行数
    /// </summary>
    public int row;

    /// <summary>
    /// 走路动画的名字
    /// </summary>
    public string walkName;
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
    protected Transform bodyTransform;
    /// <summary>
    /// 头组件
    /// </summary>
    protected Transform headTransform;
    /// <summary>
    /// 身体中心组件
    /// </summary>
    public Transform bodyCenterTransform;
    /// <summary>
    /// 身体动画组件
    /// </summary>
    protected Animator bodyAnimator;
    /// <summary>
    /// 头动画组件
    /// </summary>
    protected Animator headAnimator;

    /// <summary>
    /// 目标植物的Plant组件
    /// </summary>
    protected Plant aimPlant;

    /// <summary>
    /// 身体的SpriteRenderer组件
    /// </summary>
    protected SpriteRenderer spriteRenderer;

    private AudioSource audioSource;

    /// <summary>
    /// 死亡方式
    /// </summary>
    public int dieMode;

    /// <summary>
    /// 僵尸状态枚举
    /// </summary>
    public enum ZombieState
    {
        Walking,     // 走路
        Attacking,   // 攻击
        Dead,        // 死亡
        EatingBrain  // 吃脑子
    }

    /// <summary>
    /// 僵尸状态
    /// </summary>
    protected ZombieState state;

    /// <summary>
    /// 僵尸状态
    /// </summary>
    protected ZombieState State
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
                case ZombieState.EatingBrain:
                    StartCoroutine(EatBrain());
                    break;
                case ZombieState.Dead:
                    StartCoroutine(Die());
                    break;
            }
        }
    }

    protected void Awake()
    {
        bodyTransform = transform.Find("Body");
        headTransform = transform.Find("Head");
        bodyCenterTransform = bodyTransform.Find("Center");
        bodyAnimator = bodyTransform.GetComponent<Animator>();
        headAnimator = headTransform.GetComponent<Animator>();
        spriteRenderer = bodyTransform.GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    public virtual void Init(ZombieInfo zombieInfo, int row, int sortingOrder, int walkIndex = 0)
    {
        GetComponent<BoxCollider2D>().enabled = true;
        this.zombieInfo = zombieInfo;
        currentHP = zombieInfo.HP;
        this.row = row;
        bodyTransform.localPosition = new Vector3(0, 0.47f);
        headTransform.GetComponent<SpriteRenderer>().sortingOrder = 
            spriteRenderer.sortingOrder = sortingOrder;
        headTransform.gameObject.SetActive(false);
        if (walkIndex > 0) walkName = "Zombie_Walk" + walkIndex;

        State = ZombieState.Walking;
    }

    /// <summary>
    /// 受到伤害
    /// </summary>
    /// <param name="damage">伤害值</param>
    public virtual void GetHurt(int damage)
    {
        if (currentHP <= 0) return;
        currentHP -= damage;
        StartCoroutine(Shine());
        if (currentHP <= zombieInfo.dieHP && State != ZombieState.Dead)
            State = ZombieState.Dead;
    }

    protected IEnumerator Shine()
    {
        spriteRenderer.material.color = new Color(0.8f, 0.8f, 0.8f, 1);
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.material.color = Color.white;
    }

    /// <summary>
    /// 走路
    /// </summary>
    /// <returns></returns>
    protected IEnumerator Walk()
    {
        bodyAnimator.CrossFadeInFixedTime(walkName, 0.2f);
        dieMode = 0;
        while (State == ZombieState.Walking)
        {
            transform.Translate(Vector3.left * zombieInfo.speed * Time.deltaTime);

            yield return 0;
        }
    }

    /// <summary>
    /// 吃植物
    /// </summary>
    /// <returns></returns>
    protected IEnumerator Attack()
    {
        audioSource.clip = GameController.Instance.audioClipConf.zombieEatingClip;
        
        bodyAnimator.CrossFadeInFixedTime(attackName, 0.2f);
        dieMode = 1;
        float timer = zombieInfo.attackInterval;
        yield return new WaitForSeconds(0.4f);
        while (State == ZombieState.Attacking)
        {
            if (!aimPlant.gameObject.activeInHierarchy || aimPlant.currentHP <= 0)
            {
                State = ZombieState.Walking;
            }
            timer += Time.deltaTime;
            if (timer >= zombieInfo.attackInterval)
            {
                audioSource.Play();
                aimPlant.GetHurt(zombieInfo.damage);
                timer = 0;
            }
            yield return 0;
        }
    }

    /// <summary>
    /// 死亡
    /// </summary>
    /// <returns></returns>
    protected IEnumerator Die()
    {
        ZombieManager.Instance.RemoveZombie(this);
        if (dieMode < 2)
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
            headTransform.gameObject.SetActive(true);
            headAnimator.Play(lostHeadName);
            yield return new WaitForSeconds(1.5f);
            GetComponent<BoxCollider2D>().enabled = false;
            headAnimator.Play("Default");
            bodyAnimator.Play(dieName);
            yield return new WaitForSeconds(2f);
        }
        else if (dieMode == 2)
        {
            headTransform.gameObject.SetActive(true);
            GetComponent<BoxCollider2D>().enabled = false;
            bodyAnimator.Play(dieName);
            headAnimator.Play(lostHeadName);
            yield return new WaitForSeconds(2f);
        }
        else if (dieMode == 3)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            bodyAnimator.Play(boomDieName);
            yield return new WaitForSeconds(2f);
        }
        SelfDestroy();
    }

    /// <summary>
    /// 失去脑袋走路
    /// </summary>
    /// <returns></returns>
    protected IEnumerator LostHeadWalk()
    {
        float timer = 0;
        while (timer < 1.5f)
        {
            bodyTransform.Translate(Vector3.left * zombieInfo.speed * Time.deltaTime);
            timer += Time.deltaTime;
            yield return 0;
        }
    }

    /// <summary>
    /// 去吃脑子
    /// </summary>
    /// <returns></returns>
    protected IEnumerator EatBrain()
    {
        bodyAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
        Vector3 startPos = transform.position;
        Vector3 houseDoorPos = new Vector3(-9.52f, -0.91f);
        float timer = 0;
        while (timer < 2)
        {
            timer += Time.unscaledDeltaTime;
            transform.position = Vector3.Lerp(startPos, houseDoorPos, timer / 2);
            yield return 0;
        }
        gameObject.SetActive(false);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Plant")
        {
            aimPlant = collision.GetComponent<Plant>();
            State = ZombieState.Attacking;
        }
        else if (collision.tag == "LeftBoundary")
        {
            State = ZombieState.EatingBrain;
            GameController.Instance.State = GameController.GameState.GameOver;
        }
    }

    protected void SelfDestroy()
    {
        StopAllCoroutines();
        PoolManager.Instance.PushGameObject(gameObject, zombieInfo.prefab);
    }    
}