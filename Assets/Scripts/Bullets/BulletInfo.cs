using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
/// <summary>
/// 子弹信息
/// </summary>
public class BulletInfo
{
    /// <summary>
    /// 子弹种类
    /// </summary>
    public BulletType bulletType;

    /// <summary>
    /// 子弹预制体
    /// </summary>
    public GameObject prefab;

    /// <summary>
    /// 子弹通常图片
    /// </summary>
    public Sprite normalSprite;

    /// <summary>
    /// 子弹击中图片
    /// </summary>
    public Sprite hitSprite;

    /// <summary>
    /// 移动速度
    /// </summary>
    public float speed;

    /// <summary>
    /// 旋转速度
    /// </summary>
    public float rotateSpeed;

    /// <summary>
    /// 伤害值
    /// </summary>
    public int damage;
}
