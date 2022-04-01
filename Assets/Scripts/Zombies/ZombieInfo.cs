using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
/// <summary>
/// ��ʬ��Ϣ
/// </summary>
public class ZombieInfo
{
    /// <summary>
    /// ����
    /// </summary>
    public string name;

    /// <summary>
    /// ��ʬ����
    /// </summary>
    public ZombieType zombieType;

    /// <summary>
    /// ��ʬԤ����
    /// </summary>
    public GameObject prefab;

    /// <summary>
    /// ����ֵ
    /// </summary>
    public int HP;

    /// <summary>
    /// ����ֵ
    /// </summary>
    public int dieHP;

    /// <summary>
    /// ��ʬ�ƶ��ٶ�
    /// </summary>
    public float speed;

    /// <summary>
    /// ��ʬ�����˺�
    /// </summary>
    public int damage;

    /// <summary>
    /// ��ʬ�������
    /// </summary>
    public float attackInterval;
}
