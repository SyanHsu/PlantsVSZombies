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

    public List<Zombie>[] zombieLists;

    /// <summary>
    /// 僵尸的层级顺序
    /// </summary>
    private int[] zombieLayerOrder;
    /// <summary>
    /// 僵尸的最小层级顺序
    /// </summary>
    private int[] zombieMinLayerOrder;
    /// <summary>
    /// 僵尸的最大层级顺序
    /// </summary>
    private int[] zombieMaxLayerOrder;

    /// <summary>
    /// 创建僵尸的X轴坐标
    /// </summary>
    private float createPosX = 5.78f;
    /// <summary>
    /// 创建僵尸的Y轴坐标
    /// </summary>
    private float createPosY;

    private void Awake()
    {
        Instance = this;
        zombieConf = Resources.Load<ZombieConf>("ZombieConf");
        zombieDict = new Dictionary<ZombieType, ZombieInfo>();
        CreateDict();
        zombieLists = new List<Zombie>[GridManager.Instance.gridNumY];
        zombieLayerOrder = new int[GridManager.Instance.gridNumY];
        zombieMinLayerOrder = new int[GridManager.Instance.gridNumY];
        zombieMaxLayerOrder = new int[GridManager.Instance.gridNumY];
        for (int i = 0; i < GridManager.Instance.gridNumY; i++)
        {
            zombieLists[i] = new List<Zombie>();
            zombieLayerOrder[i] = zombieMinLayerOrder[i] = 100 * (GridManager.Instance.gridNumY - i - 1);
            zombieMaxLayerOrder[i] = 100 * (GridManager.Instance.gridNumY - i);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CreateZombie(zombieDict[ZombieType.Zombie]);
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

    public void CreateZombie(ZombieInfo zombieInfo)
    {
        // 僵尸生成的位置
        int row = Random.Range(0, GridManager.Instance.gridNumY);
        //int row = 2;
        createPosY = GridManager.Instance.GetPosY(row);
        Vector3 createPos = new Vector3(createPosX, createPosY);

        // 生成僵尸
        Zombie createdZombie = PoolManager.Instance.GetGameObject(zombieInfo.prefab, createPos, transform).GetComponent<Zombie>();
        createdZombie.Init(zombieInfo, row, zombieLayerOrder[row]++, Random.Range(1, 4));
        if (zombieLayerOrder[row] == zombieMaxLayerOrder[row]) zombieLayerOrder[row] = zombieMinLayerOrder[row];
        zombieLists[row].Add(createdZombie);
    }

    public void RemoveZombie(Zombie zombie)
    {
        zombieLists[zombie.row].Remove(zombie);
    }
}
