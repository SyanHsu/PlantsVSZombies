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
    /// 阳光数量变量
    /// </summary>
    public int sunNum;

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
