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

    /// <summary>
    /// UI阳光位置
    /// </summary>
    public Vector3 UISunPosition;

    /// <summary>
    /// 所有的UI植物卡片
    /// </summary>
    private UIPlantCard[] UIPlantCards;

    /// <summary>
    /// 卡片数目
    /// </summary>
    public int cardNum;

    private void Awake()
    {
        Instance = this;
        sunNumText = transform.Find("MainPanel/Text_SunNum").GetComponent<Text>();
        UISunPosition = new Vector3(transform.Find("MainPanel/UISun").position.x, 
            transform.Find("MainPanel/UISun").position.y);
        UIPlantCards = GetComponentsInChildren<UIPlantCard>();
        cardNum = UIPlantCards.Length;
    }

    public void Init(PlantType[] plantTypes)
    {
        for (int i = 0; i < cardNum; i++)
        {
            UIPlantCards[i].Init(plantTypes[i]);
        }
    }

    public void UpdateSunNum(int sunNum)
    {
        sunNumText.text = sunNum.ToString();
    }
}
