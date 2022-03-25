using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
/// <summary>
/// ֲ����Ϣ
/// </summary>
public class PlantInfo
{
    /// <summary>
    /// ֲ������
    /// </summary>
    public PlantType plantType;

    /// <summary>
    /// ֲ��Ԥ����
    /// </summary>
    public GameObject prefab;

    /// <summary>
    /// ֲ��ͼƬ
    /// </summary>
    public GameObject image;

    /// <summary>
    /// ֲ���͸��ͼƬ
    /// </summary>
    public GameObject translucentImage;

    /// <summary>
    /// ��Ҫ������
    /// </summary>
    public int neededSun;

    /// <summary>
    /// ��Ƭ��ȴʱ��
    /// </summary>
    public float CDTime;
}
