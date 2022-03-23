using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���տ�
/// </summary>
public class SunFlower : MonoBehaviour
{
    /// <summary>
    /// ��ʼ���������ʱ��
    /// </summary>
    public float startTime;

    /// <summary>
    /// ��ʼ���������ʱ����Сֵ
    /// </summary>
    private float startTimeMin = 4f;
    /// <summary>
    /// ��ʼ���������ʱ�����ֵ
    /// </summary>
    private float startTimeMax = 12f;

    /// <summary>
    /// �������ɼ��
    /// </summary>
    public float sunInterval = 24f;

    /// <summary>
    /// ���ɵ�����x���������
    /// </summary>
    private float createPosX;
    /// <summary>
    /// ���ɵ�����x�����������Сֵ
    /// </summary>
    private float createPosXMin = -0.6f;
    /// <summary>
    /// ���ɵ�����x������������ֵ
    /// </summary>
    private float createPosXMax = 0.6f;

    /// <summary>
    /// ���ɵ�����y���������
    /// </summary>
    private float createPosY;
    /// <summary>
    /// ���ɵ�����y�����������Сֵ
    /// </summary>
    private float createPosYMin = -0.6f;
    /// <summary>
    /// ���ɵ�����y������������ֵ
    /// </summary>
    private float createPosYMax = 0f;

    /// <summary>
    /// ��������߶�
    /// </summary>
    private float raiseHeight;
    /// <summary>
    /// ��������߶���Сֵ
    /// </summary>
    private float raiseHeightMin = 0.6f;
    /// <summary>
    /// ��������߶����ֵ
    /// </summary>
    private float raiseHeightMax = 1.2f;

    private void Start()
    {
        startTime = Random.Range(startTimeMin, startTimeMax);
        InvokeRepeating("CreateSun", startTime, sunInterval);
    }

    /// <summary>
    /// ��������
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
