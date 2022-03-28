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
    /// ����
    /// </summary>
    public string name;

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
    /// ��ƬͼƬ
    /// </summary>
    public Sprite card;

    /// <summary>
    /// ��Ҫ������
    /// </summary>
    public int neededSun;

    /// <summary>
    /// ��Ƭ��ȴʱ��
    /// </summary>
    public float CDTime;

    /// <summary>
    /// ����ֵ
    /// </summary>
    public int HP;
}
