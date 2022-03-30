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

    private void Awake()
    {
        Instance = this;
        zombieConf = Resources.Load<ZombieConf>("ZombieConf");
        zombieDict = new Dictionary<ZombieType, ZombieInfo>();
        CreateDict();
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
