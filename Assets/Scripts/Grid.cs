using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����
/// </summary>
public class Grid
{
    /// <summary>
    /// x�᷽�򳤶�
    /// </summary>
    public static readonly float lengthX = 1.33f;
    /// <summary>
    /// y�᷽�򳤶�
    /// </summary>
    public static readonly float lengthY = 1.63f;

    /// <summary>
    /// ����
    /// </summary>
    public Vector2 point;

    /// <summary>
    /// ���ĵ�λ��
    /// </summary>
    public Vector2 pos;

    /// <summary>
    /// ��߽�x����
    /// </summary>
    public float leftPosX;

    /// <summary>
    /// �ѱ���ֲ
    /// </summary>
    public bool planted;

    /// <summary>
    /// ��ֲ��ֲ��
    /// </summary>
    public Plant plantedPlant;

    /// <summary>
    /// �����캯��
    /// </summary>
    /// <param name="point">��������</param>
    /// <param name="pos">�������ĵ�λ��</param>
    public Grid(Vector2 point, Vector2 pos)
    {
        this.point = point;
        this.pos = pos;
        leftPosX = pos.x - lengthX / 2;
        planted = false;
        plantedPlant = null;
    }
}
