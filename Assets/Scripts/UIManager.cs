using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI管理器
/// </summary>
public class UIManager : MonoBehaviour
{
    /// <summary>
    /// 单例模式
    /// </summary>
    public static UIManager Instance;

    /// <summary>
    /// 记录阳光数的Text组件
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
