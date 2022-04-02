using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ʬ����
/// </summary>
public class ZombieManager : MonoBehaviour
{
    /// <summary>
    /// ����ģʽ
    /// </summary>
    public static ZombieManager Instance;

    /// <summary>
    /// ��ʬ����
    /// </summary>
    public ZombieConf zombieConf;

    /// <summary>
    /// ��ʬ�ֵ䣬ͨ����ʬ�����ҽ�ʬ
    /// ��Ϣ
    /// </summary>
    public Dictionary<ZombieType, ZombieInfo> zombieDict;

    public List<Zombie>[] zombieLists;

    /// <summary>
    /// ��ʬ�Ĳ㼶˳��
    /// </summary>
    private int[] zombieLayerOrder;
    /// <summary>
    /// ��ʬ����С�㼶˳��
    /// </summary>
    private int[] zombieMinLayerOrder;
    /// <summary>
    /// ��ʬ�����㼶˳��
    /// </summary>
    private int[] zombieMaxLayerOrder;

    /// <summary>
    /// ������ʬ��X������
    /// </summary>
    private float createPosX = 5.78f;
    /// <summary>
    /// ������ʬ��Y������
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
    /// �����ֵ�
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
        // ��ʬ���ɵ�λ��
        int row = Random.Range(0, GridManager.Instance.gridNumY);
        //int row = 2;
        createPosY = GridManager.Instance.GetPosY(row);
        Vector3 createPos = new Vector3(createPosX, createPosY);

        // ���ɽ�ʬ
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
