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
            if (state == PeaShooterState.Shooting)
                StartCoroutine(Shoot());
        }
    }

    /// <summary>
    /// 射击间隔
    /// </summary>
    private float shootInterval = 1.4f;

    private void Update()
    {
        
    }

    private IEnumerator Shoot()
    {
        while (State == PeaShooterState.Shooting)
        {
            yield return new WaitForSeconds(shootInterval);
        }
    }
}
