using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    /// <summary>
    /// 菜单按钮
    /// </summary>
    private Button menuButton;
    private Image menuButtonImage;

    private AudioSource audioSource;

    /// <summary>
    /// 主面板
    /// </summary>
    public GameObject mainPanelGO;
    /// <summary>
    /// 菜单面板
    /// </summary>
    public GameObject menuPanelGO;
    /// <summary>
    /// 死亡面板
    /// </summary>
    public GameObject diePanelGO;
    /// <summary>
    /// 胜利面板
    /// </summary>
    public GameObject winPanelGO;

    private void Awake()
    {
        Instance = this;
        sunNumText = transform.Find("Canvas_MainPanel/MainPanel/Text_SunNum").GetComponent<Text>();
        UIPlantCards = GetComponentsInChildren<UIPlantCard>();
        cardNum = UIPlantCards.Length;
        menuButton = transform.Find("Canvas_MainPanel/MainPanel/MenuButton").GetComponent<Button>();
        menuButtonImage = transform.Find("Canvas_MainPanel/MainPanel/MenuButton").GetComponent<Image>();
        mainPanelGO = transform.Find("Canvas_MainPanel").gameObject;
        menuPanelGO = transform.Find("Canvas_MenuPanel").gameObject;
        diePanelGO = transform.Find("Canvas_DiePanel").gameObject;
        winPanelGO = transform.Find("Canvas_WinPanel").gameObject;
        audioSource = GetComponent<AudioSource>();
    }

    public void Init(PlantType[] plantTypes)
    {
        UISunPosition = new Vector3(transform.Find("Canvas_MainPanel/MainPanel/UISun").position.x,
            transform.Find("Canvas_MainPanel/MainPanel/UISun").position.y);
        for (int i = 0; i < cardNum; i++)
        {
            UIPlantCards[i].Init(plantTypes[i]);
        }
    }

    public void UpdateSunNum(int sunNum)
    {
        sunNumText.text = sunNum.ToString();
    }

    public void MenuButtonOnClick()
    {
        audioSource.clip = GameController.Instance.audioClipConf.pauseClip;
        audioSource.Play();
        Time.timeScale = 0;
        menuButton.interactable = false;
        menuButtonImage.raycastTarget = false;
        menuPanelGO.SetActive(true);
    }

    public void RestartButtonOnClick()
    {
        Time.timeScale = 1;
        CursorManager.Instance.SetCursorNormal();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenuButtonOnClick()
    {
        CursorManager.Instance.SetCursorNormal();
        SceneManager.LoadScene("Scene_MainMenu");
    }

    public void ReturnGameButtonOnClick()
    {
        menuPanelGO.SetActive(false);
        menuButtonImage.raycastTarget = true;
        menuButton.interactable = true;
        Time.timeScale = 1;
        CursorManager.Instance.SetCursorNormal();
    }
}
