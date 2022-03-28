using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
/// <summary>
/// 植物信息
/// </summary>
public class PlantInfo
{
    /// <summary>
    /// 名字
    /// </summary>
    public string name;

    /// <summary>
    /// 植物种类
    /// </summary>
    public PlantType plantType;

    /// <summary>
    /// 植物预制体
    /// </summary>
    public GameObject prefab;

    /// <summary>
    /// 植物图片
    /// </summary>
    public GameObject image;

    /// <summary>
    /// 卡片图片
    /// </summary>
    public Sprite card;

    /// <summary>
    /// 需要的阳光
    /// </summary>
    public int neededSun;

    /// <summary>
    /// 卡片冷却时间
    /// </summary>
    public float CDTime;

    /// <summary>
    /// 生命值
    /// </summary>
    public int HP;
}
