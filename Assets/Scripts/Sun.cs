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
    /// 阳光的状态枚举
    /// </summary>
    private enum State
    {
        Idle,
        Falling,
        Raising,
        Flying
    }

    /// <summary>
    /// 阳光状态
    /// </summary>
    private State state;

    /// <summary>
    /// 提供的阳光数量
    /// </summary>
    private int sunNum = 25;

    /// <summary>
    /// 升起的位置
    /// </summary>
    private Vector3 raisePos;

    /// <summary>
    /// 升起速度
    /// </summary>
    private float raiseSpeed = 10f;

    /// <summary>
    /// 停止下落的高度
    /// </summary>
    private float stopFallingPosY;

    /// <summary>
    /// 下落速度
    /// </summary>
    private float fallSpeed;

    /// <summary>
    /// 飞行速度
    /// </summary>
    public float flySpeed = 10f;

    /// <summary>
    /// 最大停留时间
    /// </summary>
    private float maxStayTime = 7f;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sortingOrder = sunSortingLayer++;
    }

    private void Update()
    {
        switch (state)
        {
            case State.Falling:
                Fall();
                break;
            case State.Raising:
                Raise();
                break;
            case State.Flying:
                Fly();
                break;
        }
    }

    /// <summary>
    /// 下落
    /// </summary>
    private void Fall()
    {
        // 若未下落到指定高度，则下落，否则调用基类Update等待指定时间后消失
        if (transform.position.y > stopFallingPosY)
        {
            transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject, maxStayTime);
            state = State.Idle;
        }
    }

    /// <summary>
    /// 升起
    /// </summary>
    private void Raise()
    {
        // 若未升到指定高度，则继续升起，否则更改状态为下落
        if (Vector3.Distance(transform.position, raisePos) < 0.1f)
        {
            state = State.Falling;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, raisePos,
                raiseSpeed * Time.deltaTime);
        }
    }

    /// <summary>
    /// 向UI的太阳处飞
    /// </summary>
    private void Fly()
    {
        if (Vector3.Distance(transform.position, GameController.Instance.UISunPosition) < 0.1f)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, GameController.Instance.UISunPosition,
                flySpeed * Time.deltaTime);
        }
    }

    /// <summary>
    /// 鼠标点击阳光
    /// </summary>
    private void OnMouseDown()
    {
        state = State.Flying;
        GameController.Instance.SunNum += sunNum;
    }

    /// <summary>
    /// 对于天空掉落的阳光初始化
    /// </summary>
    /// <param name="posY">阳光停止掉落的位置</param>
    public void SkySunInit(float posY)
    {
        stopFallingPosY = posY;
        fallSpeed = 1f;
        state = State.Falling;
    }

    /// <summary>
    /// 对于植物掉落的阳光初始化
    /// </summary>
    /// <param name="posY">阳光停止掉落的位置</param>
    public void PlantSunInit(Vector3 pos, float posY)
    {
        raisePos = pos;
        stopFallingPosY = posY;
        fallSpeed = 5f;
        state = State.Raising;
    }
}