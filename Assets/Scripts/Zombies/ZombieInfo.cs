using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
/// <summary>
/// 僵尸信息
/// </summary>
public class ZombieInfo
{
    /// <summary>
    /// 名字
    /// </summary>
    public string name;

    /// <summary>
    /// 僵尸种类
    /// </summary>
    public ZombieType zombieType;

    /// <summary>
    /// 僵尸预制体
    /// </summary>
    public GameObject prefab;

    /// <summary>
    /// 生命值
    /// </summary>
    public int HP;

    /// <summary>
    /// 生命值
    /// </summary>
    public int dieHP;

    /// <summary>
    /// 僵尸移动速度
    /// </summary>
    public float speed;

    /// <summary>
    /// 僵尸单次伤害
    /// </summary>
    public int damage;

    /// <summary>
    /// 僵尸攻击间隔
    /// </summary>
    public float attackInterval;
}
