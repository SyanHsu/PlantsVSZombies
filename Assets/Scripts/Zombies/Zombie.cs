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
    protected Transform bodyTransform;
    /// <summary>
    /// ͷ���
    /// </summary>
    protected Transform headTransform;
    /// <summary>
    /// �����������
    /// </summary>
    public Transform bodyCenterTransform;
    /// <summary>
    /// ���嶯�����
    /// </summary>
    protected Animator bodyAnimator;
    /// <summary>
    /// ͷ�������
    /// </summary>
    protected Animator headAnimator;

    /// <summary>
    /// Ŀ��ֲ���Plant���
    /// </summary>
    protected Plant aimPlant;

    /// <summary>
    /// �����SpriteRenderer���
    /// </summary>
    protected SpriteRenderer spriteRenderer;

    private AudioSource audioSource;

    /// <summary>
    /// ������ʽ
    /// </summary>
    public int dieMode;

    /// <summary>
    /// ��ʬ״̬ö��
    /// </summary>
    public enum ZombieState
    {
        Walking,     // ��·
        Attacking,   // ����
        Dead,        // ����
        EatingBrain  // ������
    }

    /// <summary>
    /// ��ʬ״̬
    /// </summary>
    protected ZombieState state;

    /// <summary>
    /// ��ʬ״̬
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
    /// �ܵ��˺�
    /// </summary>
    /// <param name="damage">�˺�ֵ</param>
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
    /// ��·
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
    /// ��ֲ��
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
    /// ����
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
    /// ʧȥ�Դ���·
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
    /// ȥ������
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