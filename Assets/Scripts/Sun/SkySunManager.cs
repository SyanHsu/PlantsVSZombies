using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������µ��������
/// </summary>
public class SkySunManager : MonoBehaviour
{
    /// <summary>
    /// ����ģʽ
    /// </summary>
    public static SkySunManager Instance;

    /// <summary>
    /// ��ʼ���������ʱ��
    /// </summary>
    private float startTime = 7f;

    /// <summary>
    /// �������ɼ��
    /// </summary>
    public float sunInterval = 7f;

    /// <summary>
    /// ���������X������
    /// </summary>
    private float createPosX;
    /// <summary>
    /// ���������X��������Сֵ
    /// </summary>
    private float createPosXMin = -7f;
    /// <summary>
    /// ���������X���������ֵ
    /// </summary>
    private float createPosXMax = 4f;

    /// <summary>
    /// ���������Y������
    /// </summary>
    private float createPosY = 3.5f;

    /// <summary>
    /// ����ֹͣ����ʱ��Y��������Сֵ
    /// </summary>
    private float stopPosYMin = -4f;
    /// <summary>
    /// ����ֹͣ����ʱ��Y���������ֵ
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
    /// ��������
    /// </summary>
    private void CreateSun()
    {
        // ���������λ��
        createPosX = Random.Range(createPosXMin, createPosXMax);
        Vector3 createPos = new Vector3(createPosX, createPosY);

        // ��������
        Sun createdSun = PoolManager.Instance.GetGameObject
            (PlantManager.Instance.plantConf.sunPrefab, createPos, transform).GetComponent<Sun>();
        createdSun.SkySunInit(Random.Range(stopPosYMin, stopPosYMax));
    }
}
