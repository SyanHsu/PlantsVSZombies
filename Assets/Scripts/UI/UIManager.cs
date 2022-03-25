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

    /// <summary>
    /// UI����λ��
    /// </summary>
    public Vector3 UISunPosition;

    private void Awake()
    {
        Instance = this;
        sunNumText = transform.Find("MainPanel/Text_SunNum").GetComponent<Text>();
        UISunPosition = new Vector3(transform.Find("MainPanel/UISun").position.x, 
            transform.Find("MainPanel/UISun").position.y);
    }

    public void UpdateSunNum(int sunNum)
    {
        sunNumText.text = sunNum.ToString();
    }
}
