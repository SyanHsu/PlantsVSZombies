using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 铲子管理
/// </summary>
public class ShovelManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    /// <summary>
    /// 铲子正在使用
    /// </summary>
    private bool isUsing;

    /// <summary>
    /// 铲子正在使用
    /// </summary>
    private bool IsUsing
    {
        get => isUsing;
        set
        {
            isUsing = value;
            // 当设置卡片状态时调用相应的协程
            if (isUsing) StartCoroutine(UseShovel());
        }
    }

    private GameObject shovelGO;

    /// <summary>
    /// 鼠标按钮按下
    /// </summary>
    private int mouseButtonPressed;

    private void Awake()
    {
        // 得到对应的铲子组件
        shovelGO = transform.Find("Shovel").gameObject;
        IsUsing = false;
    }


    /// <summary>
    /// 使用铲子
    /// </summary>
    /// <returns></returns>
    private IEnumerator UseShovel()
    {
        // 原铲子不可见
        shovelGO.SetActive(false);

        // 跟随鼠标的图片
        GameObject shovelImage = PoolManager.Instance.GetGameObject(PlantManager.Instance.plantConf.shovelImage, Vector3.zero, PlantManager.Instance.transform);
        shovelImage.transform.rotation = Quaternion.Euler(0, 0, 45);
        // 鼠标指针不可改变
        CursorManager.Instance.changable = false;

        // 得到鼠标位置
        Vector3 mousePos;

        // 图片相对鼠标偏移量
        float deltaX = 0.5f;
        float deltaY = 0.45f;

        // 要铲除的网格
        Grid grid;

        while (IsUsing)
        {
            // 获取距离鼠标最近的网格
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            shovelImage.transform.position = new Vector3(mousePos.x + deltaX, mousePos.y + deltaY);
            grid = GridManager.Instance.GetNearestGrid(mousePos);

            if (mouseButtonPressed != -1)
            {
                if (Input.GetMouseButtonUp(mouseButtonPressed))
                {
                    mouseButtonPressed = -1;
                }
            }
            //按下鼠标左键时，若网格合理则铲除植物，否则恢复铲子状态；按下鼠标右键时，恢复铲子状态
            else if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                if (Input.GetMouseButtonDown(0) && grid != null && grid.planted)
                {
                    PlantManager.Instance.RemovePlant(grid.plantedPlant);
                    
                }
                PoolManager.Instance.PushGameObject(shovelImage, PlantManager.Instance.plantConf.shovelImage);
                IsUsing = false;
            }
            yield return 0;
        }

        // 改回玩家状态
        CursorManager.Instance.changable = true;

        // 恢复铲子
        shovelGO.SetActive(true);
    }

    /// <summary>
    /// 鼠标进入
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!CursorManager.Instance.changable || Time.timeScale == 0) return;

        CursorManager.Instance.SetCursorLink();
    }

    /// <summary>
    /// 鼠标退出
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        if (Time.timeScale == 0) return;

        CursorManager.Instance.SetCursorNormal();
    }

    /// <summary>
    /// 鼠标点击
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!CursorManager.Instance.changable || Time.timeScale == 0) return;

        mouseButtonPressed = (int)eventData.button;
        IsUsing = true;
    }
}