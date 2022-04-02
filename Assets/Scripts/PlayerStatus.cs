using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家状态
/// </summary>
public class PlayerStatus : MonoBehaviour
{
    /// <summary>
    /// 单例模式
    /// </summary>
    public static PlayerStatus Instance;

    /// <summary>
    /// 玩家状态枚举
    /// </summary>
    public enum PlayerState
    {
        Default,    // 默认
        Planting    // 种植中
    }

    /// <summary>
    /// 玩家状态
    /// </summary>
    public PlayerState state = PlayerState.Default;

    /// <summary>
    /// 阳光数量变量
    /// </summary>
    public int sunNum = 1000;

    /// <summary>
    /// 阳光数量属性
    /// </summary>
    public int SunNum
    {
        get => sunNum;
        set
        {
            sunNum = value;
            // 将更改传递至UI
            UIManager.Instance.UpdateSunNum(sunNum);
        }
    }

    private void Awake()
    {
        Instance = this;
    }
}
