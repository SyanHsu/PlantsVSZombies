using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 网格
/// </summary>
public class Grid
{
    /// <summary>
    /// x轴方向长度
    /// </summary>
    public static readonly float lengthX = 1.33f;
    /// <summary>
    /// y轴方向长度
    /// </summary>
    public static readonly float lengthY = 1.63f;

    /// <summary>
    /// 坐标
    /// </summary>
    public Vector2 point;

    /// <summary>
    /// 中心点位置
    /// </summary>
    public Vector2 pos;

    /// <summary>
    /// 已被种植
    /// </summary>
    public bool planted;

    /// <summary>
    /// 网格构造函数
    /// </summary>
    /// <param name="point">网格坐标</param>
    /// <param name="pos">网格中心点位置</param>
    public Grid(Vector2 point, Vector2 pos)
    {
        this.point = point;
        this.pos = pos;
        planted = false;
    }
}
