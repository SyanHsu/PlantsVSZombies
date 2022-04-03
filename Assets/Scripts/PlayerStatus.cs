using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���״̬
/// </summary>
public class PlayerStatus : MonoBehaviour
{
    /// <summary>
    /// ����ģʽ
    /// </summary>
    public static PlayerStatus Instance;

    /// <summary>
    /// ������������
    /// </summary>
    public int sunNum;

    /// <summary>
    /// ������������
    /// </summary>
    public int SunNum
    {
        get => sunNum;
        set
        {
            sunNum = value;
            // �����Ĵ�����UI
            UIManager.Instance.UpdateSunNum(sunNum);
        }
    }

    private void Awake()
    {
        Instance = this;
    }
}
