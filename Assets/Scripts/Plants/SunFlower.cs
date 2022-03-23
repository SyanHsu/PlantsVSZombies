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
    public float startTime;

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
    public float sunInterval = 24f;

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
        startTime = Random.Range(startTimeMin, startTimeMax);
        InvokeRepeating("CreateSun", startTime, sunInterval);
    }

    /// <summary>
    /// 产生阳光
    /// </summary>
    private void CreateSun()
    {
        GameObject createdSun = Instantiate<GameObject>(GameController.Instance.Prefab_Sun,
            transform.position, Quaternion.identity);
        createPosX = Random.Range(createPosXMin, createPosXMax);
        createPosY = Random.Range(createPosYMin, createPosYMax);
        raiseHeight = Random.Range(raiseHeightMin, raiseHeightMax);
        Vector3 raisePos = new Vector3(transform.position.x + createPosX, transform.position.y + raiseHeight);
        createdSun.GetComponent<Sun>().PlantSunInit(raisePos, transform.position.y + createPosY);
    }
}
