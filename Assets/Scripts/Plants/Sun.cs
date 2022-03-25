using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 阳光
/// </summary>
public class Sun : MonoBehaviour
{
    /// <summary>
    /// 阳光层级
    /// </summary>
    public static int sunSortingLayer = 0;

    /// <summary>
    /// 最大停留时间
    /// </summary>
    private float maxStayTime = 7f;

    /// <summary>
    /// 阳光状态枚举
    /// </summary>
    private enum SunState
    {
        Idle,     // 闲置
        Falling,  // 下落
        Raising,  // 升起（对植物产生的阳光）
        Flying    // 飞行（至UI）
    }

    /// <summary>
    /// 阳光状态
    /// </summary>
    private SunState state;

    /// <summary>
    /// 阳光状态
    /// </summary>
    private SunState State
    {
        get => state;
        set
        {
            state = value;
            // 当设置阳光状态时调用相应的协程
            switch (state)
            {
                case SunState.Idle:
                    // 一定时间后销毁自身
                    Destroy(gameObject, maxStayTime);
                    break;
                case SunState.Falling:
                    StartCoroutine(Fall());
                    break;
                case SunState.Raising:
                    StartCoroutine(Raise());
                    break;
                case SunState.Flying:
                    StartCoroutine(Fly());
                    break;
            }
        }
    }

    /// <summary>
    /// 提供的阳光数量
    /// </summary>
    private int sunNum = 25;

    /// <summary>
    /// 升起的位置
    /// </summary>
    private Vector3 raisePos;

    /// <summary>
    /// 停止下落的高度
    /// </summary>
    private float stopFallingPosY;

    /// <summary>
    /// 升起速度
    /// </summary>
    private float raiseSpeed = 10f;

    /// <summary>
    /// 下落速度
    /// </summary>
    private float fallSpeed;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sortingOrder = sunSortingLayer++;
    }

    /// <summary>
    /// 对于天空掉落的阳光初始化
    /// </summary>
    /// <param name="posY">阳光停止掉落的位置</param>
    public void SkySunInit(float posY)
    {
        // 调整参数
        stopFallingPosY = posY;
        fallSpeed = 1f;

        // 初始状态为下落
        State = SunState.Falling;
    }

    /// <summary>
    /// 对于植物掉落的阳光初始化
    /// </summary>
    /// <param name="posY">阳光停止掉落的位置</param>
    public void PlantSunInit(Vector3 pos, float posY)
    {
        // 调整参数
        raisePos = pos;
        stopFallingPosY = posY;
        fallSpeed = 5f;

        // 初始状态为升起
        State = SunState.Raising;
    }

    /// <summary>
    /// 下落
    /// </summary>
    private IEnumerator Fall()
    {
        // 若未下落到指定高度，则下落
        while (transform.position.y > stopFallingPosY)
        {
            transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
            yield return 0;
        }

        // 调整状态为闲置
        State = SunState.Idle;
    }

    /// <summary>
    /// 升起
    /// </summary>
    private IEnumerator Raise()
    {
        // 若未升到指定高度，则继续升起
        while (Vector3.Distance(transform.position, raisePos) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, raisePos,
                raiseSpeed * Time.deltaTime);
            yield return 0;
        }

        // 调整状态为下路
        State = SunState.Falling;
    }

    /// <summary>
    /// 向UI的太阳处飞
    /// </summary>
    private IEnumerator Fly()
    {
        float flySpeed = 8f;
        SpriteRenderer sunSprite = GetComponent<SpriteRenderer>();
        Color transColor = new Color(1, 1, 1, 0);
        float leftDist;
        // 若未到UI处，则继续飞
        do
        {
            transform.position = Vector3.Lerp(transform.position, UIManager.Instance.UISunPosition,
                flySpeed * Time.deltaTime);
            leftDist = Vector3.Distance(transform.position, UIManager.Instance.UISunPosition);
            if (leftDist < 0.5f) sunSprite.material.color = new Color(1, 1, 1, leftDist + 0.4f);
            yield return 0;
        } while (leftDist > 0.2f);

        // 销毁自身
        Destroy(gameObject);
    }

    /// <summary>
    /// 鼠标进入
    /// </summary>
    private void OnMouseEnter()
    {
        if (PlayerStatus.Instance.state == PlayerStatus.PlayerState.Planting) return;

        // 设置鼠标指针
        GameController.Instance.SetCursorLink();
    }

    /// <summary>
    /// 鼠标退出
    /// </summary>
    private void OnMouseExit()
    {
        if (PlayerStatus.Instance.state == PlayerStatus.PlayerState.Planting) return;

        // 设置鼠标指针
        GameController.Instance.SetCursorNormal();
    }

    /// <summary>
    /// 鼠标点击
    /// </summary>
    private void OnMouseDown()
    {
        if (PlayerStatus.Instance.state == PlayerStatus.PlayerState.Planting) return;

        // 状态更改为飞行
        State = SunState.Flying;

        // 游戏的太阳数增加
        PlayerStatus.Instance.SunNum += sunNum;
    }
}