using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 空中落下的阳光控制
/// </summary>
public class SkySunManager : MonoBehaviour
{
    /// <summary>
    /// 单例模式
    /// </summary>
    public static SkySunManager Instance;

    /// <summary>
    /// 开始生成阳光的时间
    /// </summary>
    private float startTime = 7f;

    /// <summary>
    /// 阳光生成间隔
    /// </summary>
    public float sunInterval = 7f;

    /// <summary>
    /// 创建阳光的X轴坐标
    /// </summary>
    private float createPosX;
    /// <summary>
    /// 创建阳光的X轴坐标最小值
    /// </summary>
    private float createPosXMin = -7f;
    /// <summary>
    /// 创建阳光的X轴坐标最大值
    /// </summary>
    private float createPosXMax = 4f;

    /// <summary>
    /// 创建阳光的Y轴坐标
    /// </summary>
    private float createPosY = 3.5f;

    /// <summary>
    /// 阳光停止下落时的Y轴坐标最小值
    /// </summary>
    private float stopPosYMin = -4f;
    /// <summary>
    /// 阳光停止下落时的Y轴坐标最大值
    /// </summary>
    private float stopPosYMax = 2.5f;

    public AudioSource audioSource;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void Init()
    {
        audioSource.clip = GameController.Instance.audioClipConf.sunClip;
        InvokeRepeating("CreateSun", startTime, sunInterval);
    }

    /// <summary>
    /// 产生阳光
    /// </summary>
    private void CreateSun()
    {
        // 阳光产生的位置
        createPosX = Random.Range(createPosXMin, createPosXMax);
        Vector3 createPos = new Vector3(createPosX, createPosY);

        // 生成阳光
        Sun createdSun = PoolManager.Instance.GetGameObject
            (PlantManager.Instance.plantConf.sunPrefab, createPos, transform).GetComponent<Sun>();
        createdSun.SkySunInit(Random.Range(stopPosYMin, stopPosYMax));
    }
}
