using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// UI植物卡片
/// </summary>
public class UIPlantCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    /// <summary>
    /// 卡片状态枚举
    /// </summary>
    private enum CardState
    {
        CDing,       // 冷却中
        LackofSun,   // 缺少阳光
        Plantable,   // 可种植
        Planting     // 种植中
    }

    /// <summary>
    /// 卡片状态
    /// </summary>
    private CardState state;

    /// <summary>
    /// 卡片状态
    /// </summary>
    private CardState State
    {
        get => state;
        set
        {
            state = value;
            // 当设置卡片状态时调用相应的协程
            switch (state)
            {
                case CardState.CDing:
                    StartCoroutine(EnterCD());
                    break;
                case CardState.LackofSun:
                    StartCoroutine(WaitingForSun());
                    break;
                case CardState.Plantable:
                    StartCoroutine(WaitingToBePlanted());
                    break;
                case CardState.Planting:
                    StartCoroutine(Planting());
                    break;
            }
        }
    }

    /// <summary>
    /// 灰图片组件
    /// </summary>
    private Image grayImage;
    /// <summary>
    /// 暗图片组件
    /// </summary>
    private Image darkImage;
    /// <summary>
    /// 亮图片组件
    /// </summary>
    private Image brightImage;
    /// <summary>
    /// 解释文本
    /// </summary>
    private GameObject explGO;

    /// <summary>
    /// 植物种类
    /// </summary>
    public PlantType plantType = PlantType.Default;

    /// <summary>
    /// 植物种类
    /// </summary>
    public PlantInfo plantInfo;

    /// <summary>
    /// 展示解释信息
    /// </summary>
    private bool showExpl;

    private void Start()
    {
        // 得到对应的图片组件
        Image[] images = GetComponentsInChildren<Image>();
        foreach (var item in images)
        {
            if (item.name.EndsWith("Gray"))
            {
                grayImage = item;
            }
            else if (item.name.EndsWith("Dark"))
            {
                darkImage = item;
            }
            else if (item.name.EndsWith("Bright"))
            {
                brightImage = item;
            }
        }
        explGO = transform.Find("Explaination").gameObject;

        if (plantType != PlantType.Default)
        {
            // 得到对应的植物信息
            plantInfo = PlantManager.Instance.plantDict[plantType];
            grayImage.sprite = darkImage.sprite = brightImage.sprite = plantInfo.card;

            // 初始化卡片状态为CD中
            State = CardState.CDing;
        }
    }

    /// <summary>
    /// 进入CD
    /// </summary>
    /// <returns></returns>
    private IEnumerator EnterCD()
    {
        // 显示灰图片
        brightImage.enabled = false;
        darkImage.fillAmount = 0;
        
        // 不断更新CD条
        float currentCDTime = 0;
        float deltaFillAmount = 0.1f / plantInfo.CDTime;
        while (currentCDTime < plantInfo.CDTime)
        {
            yield return new WaitForSeconds(0.1f);
            darkImage.fillAmount += deltaFillAmount;
            currentCDTime += 0.1f;
        }

        // 若阳光不够则更改状态为缺少阳光，否则更改为可种植
        if (PlayerStatus.Instance.SunNum < plantInfo.neededSun) State = CardState.LackofSun;
        else State = CardState.Plantable;
    }

    /// <summary>
    /// 等待阳光
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitingForSun()
    {
        // 显示暗图片
        brightImage.enabled = false;
        darkImage.fillAmount = 1;
        
        //等待阳光至足够
        while (PlayerStatus.Instance.SunNum < plantInfo.neededSun)
        {
            yield return 0;
        }

        // 阳光足够，更改状态为可种植
        State = CardState.Plantable;
    }

    /// <summary>
    /// 等待被种植
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitingToBePlanted()
    {
        // 显示亮图片
        brightImage.enabled = true;
        darkImage.fillAmount = 0;
        
        while (State == CardState.Plantable)
        {
            if (PlayerStatus.Instance.SunNum < plantInfo.neededSun) State = CardState.LackofSun;
            yield return 0;
        }
    }

    /// <summary>
    /// 种植中
    /// </summary>
    /// <returns></returns>
    private IEnumerator Planting()
    {
        // 显示灰图片
        brightImage.enabled = false;
        darkImage.fillAmount = 0;

        // 跟随鼠标的图片
        GameObject plantImage = Instantiate<GameObject>(plantInfo.image, Vector3.zero, 
            Quaternion.identity, PlantManager.Instance.transform);
        plantImage.GetComponent<SpriteRenderer>().sortingOrder = 1;
        // 要种植处网格的半透明图片
        GameObject plantTranslucentImage = Instantiate<GameObject>(plantInfo.image, Vector3.zero, 
            Quaternion.identity, PlantManager.Instance.transform);
        plantTranslucentImage.GetComponent<SpriteRenderer>().material.color = new Color(1, 1, 1, 0.6f);

        // 更改玩家状态
        PlayerStatus.Instance.state = PlayerStatus.PlayerState.Planting;

        // 得到鼠标位置
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // 图片相对鼠标偏移量
        float deltaX = 0.08f;
        float deltaY = 0.32f;

        // 要种植的网格
        Grid grid;

        while (State == CardState.Planting)
        {
            // 获取距离鼠标最近的网格
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            plantImage.transform.position = new Vector3(mousePos.x + deltaX, mousePos.y + deltaY);
            grid = GridManager.Instance.GetNearestGrid(mousePos);

            // 若网格合理则在网格处显示半透明图片，否则不显示
            if (grid == null || grid.planted)
            {
                plantTranslucentImage.SetActive(false);
            }
            else
            {
                plantTranslucentImage.SetActive(true);
                plantTranslucentImage.transform.position = grid.pos;
            }

            //按下鼠标左键时，若网格合理则种植植物，否则恢复可种植状态
            if (Input.GetMouseButtonDown(0))
            {
                Destroy(plantImage);
                Destroy(plantTranslucentImage);
                if (grid == null || grid.planted)
                {
                    State = CardState.Plantable;
                }
                else
                {
                    PlantManager.Instance.Plant(plantInfo, grid);
                    State = CardState.CDing;
                }
            }
            // 按下鼠标右键时，恢复可种植状态
            else if (Input.GetMouseButtonDown(1))
            {
                Destroy(plantImage);
                Destroy(plantTranslucentImage);
                State = CardState.Plantable;
            }
            yield return 0;
        }

        // 改回玩家状态
        PlayerStatus.Instance.state = PlayerStatus.PlayerState.Default;
    }

    /// <summary>
    /// 鼠标进入
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (PlayerStatus.Instance.state == PlayerStatus.PlayerState.Planting) return;

        // 显示解释信息
        showExpl = true;

        StartCoroutine(PointerStay());
    }

    private IEnumerator PointerStay()
    {
        // 显示解释文字和背景
        explGO.SetActive(true);

        while (showExpl)
        {
            // 对应卡片不同状态调整文字和背景大小
            switch (state)
            {
                case CardState.CDing:
                    explGO.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(
                        RectTransform.Axis.Horizontal, 90);
                    explGO.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(
                        RectTransform.Axis.Vertical, 34);
                    explGO.GetComponentInChildren<Text>().text =
                        "<color=red>重新装填中...</color>\n" + plantInfo.name;
                    break;
                case CardState.LackofSun:
                    explGO.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(
                        RectTransform.Axis.Horizontal, 105);
                    explGO.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(
                        RectTransform.Axis.Vertical, 34);
                    explGO.GetComponentInChildren<Text>().text =
                        "<color=red>没有足够的阳光</color>\n" + plantInfo.name;
                    break;
                case CardState.Plantable:
                    explGO.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(
                        RectTransform.Axis.Horizontal, 15 * brightImage.sprite.name.Length);
                    explGO.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(
                        RectTransform.Axis.Vertical, 17);
                    explGO.GetComponentInChildren<Text>().text = plantInfo.name;
                    // 设置鼠标指针
                    GameController.Instance.SetCursorLink();
                    break;
            }
            yield return 0;
        }

        // 取消显示解释文字和背景
        explGO.SetActive(false);

        // 设置鼠标指针
        GameController.Instance.SetCursorNormal();
    }

    /// <summary>
    /// 鼠标退出
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        if (PlayerStatus.Instance.state == PlayerStatus.PlayerState.Planting) return;

        // 不显示解释信息
        showExpl = false;
    }

    /// <summary>
    /// 鼠标点击
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (PlayerStatus.Instance.state == PlayerStatus.PlayerState.Planting) return;

        if (State != CardState.Plantable) return;

        State = CardState.Planting;

        // 不显示解释信息
        showExpl = false;
    }
}