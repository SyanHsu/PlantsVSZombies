using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����
/// </summary>
public class Grid
{
    public static readonly float lengthX = 1.33f;
    public static readonly float lengthY = 1.63f;

    /// <summary>
    /// ����
    /// </summary>
    public Vector2 point;

    /// <summary>
    /// λ��
    /// </summary>
    public Vector2 pos;

    /// <summary>
    /// �ѱ���ֲ
    /// </summary>
    public bool planted;

    public Grid(Vector2 point, Vector2 pos)
    {
        this.point = point;
        this.pos = pos;
        planted = false;
    }
}