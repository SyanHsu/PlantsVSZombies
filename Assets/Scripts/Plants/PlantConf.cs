using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ֲ������
/// </summary>
[CreateAssetMenu(fileName = "PlantConf", menuName = "PlantConf")]
public class PlantConf : ScriptableObject
{
    /// <summary>
    /// ����ֲ����Ϣ
    /// </summary>
    public PlantInfo[] plantInfos;

    /// <summary>
    /// �����ӵ���Ϣ
    /// </summary>
    public BulletInfo[] bulletInfos;

    /// <summary>
    /// ����Ԥ����
    /// </summary>
    public GameObject sunPrefab;
}
