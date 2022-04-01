using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ֲ������
/// </summary>
[CreateAssetMenu(fileName = "PlantConf", menuName = "PlantConf")]
public class PlantConf : ScriptableObject
{
    /// <summary>
    /// ����ֲ����Ϣ
    /// </summary>
    public PlantInfo[] plantInfos;

    /// <summary>
    /// �㶹Ԥ����
    /// </summary>
    public GameObject peaPrefab;

    /// <summary>
    /// �㶹������ЧͼƬ
    /// </summary>
    public Sprite peaHitSprite;

    /// <summary>
    /// ����Ԥ����
    /// </summary>
    public GameObject sunPrefab;
}
