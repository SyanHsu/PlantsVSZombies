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

    /// <summary>
    /// 僵尸的层级顺序
    /// </summary>
    private int[] zombieLayerOrder;
    /// <summary>
    /// 僵尸的最大层级顺序
    /// </summary>
    private int[] zombieMaxLayerOrder;

    private void Awake()
    {
        Instance = this;
        zombieConf = Resources.Load<ZombieConf>("ZombieConf");
        zombieDict = new Dictionary<ZombieType, ZombieInfo>();
        CreateDict();
        zombieLayerOrder = new int[GridManager.Instance.gridNumY];
        for (int i = 0; i < GridManager.Instance.gridNumY; i++)
        {
            zombieLayerOrder[i] = 100 * (GridManager.Instance.gridNumY - i - 1);
            zombieMaxLayerOrder[i] = 100 * (GridManager.Instance.gridNumY - i);
        }
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
