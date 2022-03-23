using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI������
/// </summary>
public class UIManager : MonoBehaviour
{
    /// <summary>
    /// ����ģʽ
    /// </summary>
    public static UIManager Instance;

    /// <summary>
    /// ��¼��������Text���
    /// </summary>
    private Text sunNumText;

    private void Awake()
    {
        Instance = this;
        sunNumText = transform.Find("MainPanel/Text_SunNum").GetComponent<Text>();
    }

    public void UpdateSunNum(int sunNum)
    {
        sunNumText.text = sunNum.ToString();
    }
}
