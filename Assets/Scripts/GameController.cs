using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��Ϸ������
/// </summary>
public class GameController : MonoBehaviour
{
    /// <summary>
    /// ����ģʽ
    /// </summary>
    public static GameController Instance;

    /// <summary>
    /// UI����λ��
    /// </summary>
    public Vector3 UISunPosition;

    /// <summary>
    /// ������������
    /// </summary>
    private int sunNum = 50;

    /// <summary>
    /// ������������
    /// </summary>
    public int SunNum
    {
        get => sunNum;
        set
        {
            sunNum = value;
            UIManager.Instance.UpdateSunNum(sunNum);
        }
    }

    /// <summary>
    /// ����Ԥ����
    /// </summary>
    public GameObject Prefab_Sun { get; private set; }

    /// <summary>
    /// ���տ�Ԥ����
    /// </summary>
    public GameObject Prefab_SunFlower { get; private set; }

    /// <summary>
    /// �㶹����Ԥ����
    /// </summary>
    public GameObject Prefab_PeaShooter { get; private set; }

    private void Awake()
    {
        Instance = this;
        UISunPosition = transform.Find("UISun").localPosition;
        Prefab_Sun = Resources.Load<GameObject>("Sun");
        Prefab_SunFlower = Resources.Load<GameObject>("SunFlower");
        Prefab_PeaShooter = Resources.Load<GameObject>("PeaShooter");
    }
}
