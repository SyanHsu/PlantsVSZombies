using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 网格
/// </summary>
public class Grid
{
    public static readonly float lengthX = 1.33f;
    public static readonly float lengthY = 1.63f;

    /// <summary>
    /// 坐标
    /// </summary>
    public Vector2 point;

    /// <summary>
    /// 位置
    /// </summary>
    public Vector2 pos;

    /// <summary>
    /// 已被种植
    /// </summary>
    public bool planted;

    public Grid(Vector2 point, Vector2 pos)
    {
        this.point = point;
        this.pos = pos;
        planted = false;
    }
}
