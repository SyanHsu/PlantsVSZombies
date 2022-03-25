using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 向日葵
/// </summary>
public class SunFlower : MonoBehaviour
{
    /// <summary>
    /// 开始生成阳光的时间
    /// </summary>
    private float startTime;

    /// <summary>
    /// 开始生成阳光的时间最小值
    /// </summary>
    private float startTimeMin = 4f;
    /// <summary>
    /// 开始生成阳光的时间最大值
    /// </summary>
    private float startTimeMax = 12f;

    /// <summary>
    /// 阳光生成间隔
    /// </summary>
    private float sunInterval = 24f;

    /// <summary>
    /// 生成的阳光x轴相对坐标
    /// </summary>
    private float createPosX;
    /// <summary>
    /// 生成的阳光x轴相对坐标最小值
    /// </summary>
    private float createPosXMin = -0.6f;
    /// <summary>
    /// 生成的阳光x轴相对坐标最大值
    /// </summary>
    private float createPosXMax = 0.6f;

    /// <summary>
    /// 生成的阳光y轴相对坐标
    /// </summary>
    private float createPosY;
    /// <summary>
    /// 生成的阳光y轴相对坐标最小值
    /// </summary>
    private float createPosYMin = -0.6f;
    /// <summary>
    /// 生成的阳光y轴相对坐标最大值
    /// </summary>
    private float createPosYMax = 0f;

    /// <summary>
    /// 阳光升起高度
    /// </summary>
    private float raiseHeight;
    /// <summary>
    /// 阳光升起高度最小值
    /// </summary>
    private float raiseHeightMin = 0.6f;
    /// <summary>
    /// 阳光升起高度最大值
    /// </summary>
    private float raiseHeightMax = 1.2f;

    private void Start()
    {
        // 一定范围内随机开始阳光的时间
        startTime = Random.Range(startTimeMin, startTimeMax);

        // 重复调用产生阳光的方法
        InvokeRepeating("CreateSun", startTime, sunInterval);
    }

    /// <summary>
    /// 产生阳光
    /// </summary>
    private void CreateSun()
    {
        // 生成阳光的物体
        GameObject createdSun = Instantiate<GameObject>(GameController.Instance.gameConf.Prefab_Sun,
            transform.position, Quaternion.identity);

        // 随机设置阳光产生的位置（向日葵附近）与高度并将值传递给阳光
        createPosX = Random.Range(createPosXMin, createPosXMax);
        createPosY = Random.Range(createPosYMin, createPosYMax);
        raiseHeight = Random.Range(raiseHeightMin, raiseHeightMax);
        Vector3 raisePos = new Vector3(transform.position.x + createPosX, transform.position.y + raiseHeight);
        createdSun.GetComponent<Sun>().PlantSunInit(raisePos, transform.position.y + createPosY);
    }
}