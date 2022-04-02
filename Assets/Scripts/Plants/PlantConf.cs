using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 植物配置
/// </summary>
[CreateAssetMenu(fileName = "PlantConf", menuName = "PlantConf")]
public class PlantConf : ScriptableObject
{
    /// <summary>
    /// 各种植物信息
    /// </summary>
    public PlantInfo[] plantInfos;

    /// <summary>
    /// 各种子弹信息
    /// </summary>
    public BulletInfo[] bulletInfos;

    /// <summary>
    /// 阳光预制体
    /// </summary>
    public GameObject sunPrefab;
}
