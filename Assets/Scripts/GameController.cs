using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏控制器
/// </summary>
public class GameController : MonoBehaviour
{
    /// <summary>
    /// 单例模式
    /// </summary>
    public static GameController Instance;

    /// <summary>
    /// UI阳光位置
    /// </summary>
    public Vector3 UISunPosition;

    /// <summary>
    /// 阳光数量变量
    /// </summary>
    private int sunNum = 50;

    /// <summary>
    /// 阳光数量属性
    /// </summary>
    public int SunNum
    {
        get => sunNum;
        set
        {
            sunNum = value;
            UIManager.Instance.UpdateSunNum(sunNum);
        }
    }

    /// <summary>
    /// 阳光预制体
    /// </summary>
    public GameObject Prefab_Sun { get; private set; }

    /// <summary>
    /// 向日葵预制体
    /// </summary>
    public GameObject Prefab_SunFlower { get; private set; }

    /// <summary>
    /// 豌豆射手预制体
    /// </summary>
    public GameObject Prefab_PeaShooter { get; private set; }

    private void Awake()
    {
        Instance = this;
        UISunPosition = transform.Find("UISun").localPosition;
        Prefab_Sun = Resources.Load<GameObject>("Sun");
        Prefab_SunFlower = Resources.Load<GameObject>("SunFlower");
        Prefab_PeaShooter = Resources.Load<GameObject>("PeaShooter");
    }
}
