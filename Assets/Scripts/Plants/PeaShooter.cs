using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 豌豆射手
/// </summary>
public class PeaShooter : Plant
{
    /// <summary>
    /// 豌豆射手状态枚举
    /// </summary>
    private enum PeaShooterState
    {
        Idle,      // 闲置
        Shooting   // 射击
    }

    /// <summary>
    /// 豌豆射手状态
    /// </summary>
    private PeaShooterState state;

    /// <summary>
    /// 豌豆射手状态
    /// </summary>
    private PeaShooterState State
    {
        get => state;
        set
        {
            state = value;
            // 当设置卡片状态时调用相应的协程
            switch(state)
            {
                case PeaShooterState.Idle:
                    StartCoroutine(Idle());
                    break;
                case PeaShooterState.Shooting:
                    StartCoroutine(Shoot());
                    break;
            }
        }
    }

    /// <summary>
    /// 射击间隔
    /// </summary>
    private float shootInterval = 1.4f;

    /// <summary>
    /// 射击点
    /// </summary>
    private Transform firePoint;

    private void Start()
    {
        firePoint = transform.Find("FirePoint");
        State = PeaShooterState.Idle;
    }

    /// <summary>
    /// 闲置
    /// </summary>
    /// <returns></returns>
    private IEnumerator Idle()
    {
        while (State == PeaShooterState.Idle)
        {
            yield return new WaitForSeconds(0.1f);
            if (CheckZombie()) State = PeaShooterState.Shooting;
        }
    }

    /// <summary>
    /// 射击
    /// </summary>
    /// <returns></returns>
    private IEnumerator Shoot()
    {
        while (State == PeaShooterState.Shooting)
        {
            yield return new WaitForSeconds(shootInterval);
            Instantiate<GameObject>(PlantManager.Instance.plantConf.peaPrefab, 
                firePoint.position, Quaternion.identity, transform);
            if (!CheckZombie()) State = PeaShooterState.Idle;
        }
    }

    /// <summary>
    /// 判断同一行是否有僵尸
    /// </summary>
    /// <returns>同一行是否有僵尸</returns>
    private bool CheckZombie()
    {
        return true;
    }
}
