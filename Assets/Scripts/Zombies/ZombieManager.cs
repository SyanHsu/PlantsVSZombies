using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 僵尸管理
/// </summary>
public class ZombieManager : MonoBehaviour
{
    /// <summary>
    /// 单例模式
    /// </summary>
    public static ZombieManager Instance;

    /// <summary>
    /// 僵尸配置
    /// </summary>
    public ZombieConf zombieConf;

    /// <summary>
    /// 僵尸字典，通过僵尸种类找僵尸
    /// 信息
    /// </summary>
    public Dictionary<ZombieType, ZombieInfo> zombieDict;

    private void Awake()
    {
        Instance = this;
        zombieConf = Resources.Load<ZombieConf>("ZombieConf");
        zombieDict = new Dictionary<ZombieType, ZombieInfo>();
        CreateDict();
    }

    /// <summary>
    /// 创建字典
    /// </summary>
    private void CreateDict()
    {
        foreach (var item in zombieConf.zombieInfos)
        {
            zombieDict.Add(item.zombieType, item);
        }
    }
}
