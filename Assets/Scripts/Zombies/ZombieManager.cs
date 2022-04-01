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

    /// <summary>
    /// ��ʬ�Ĳ㼶˳��
    /// </summary>
    private int[] zombieLayerOrder;
    /// <summary>
    /// ��ʬ�����㼶˳��
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
    /// �����ֵ�
    /// </summary>
    private void CreateDict()
    {
        foreach (var item in zombieConf.zombieInfos)
        {
            zombieDict.Add(item.zombieType, item);
        }
    }
}
