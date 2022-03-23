using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����
/// </summary>
public class Sun : MonoBehaviour
{
    /// <summary>
    /// ����㼶
    /// </summary>
    public static int sunSortingLayer = 0;

    /// <summary>
    /// �����״̬ö��
    /// </summary>
    private enum State
    {
        Idle,
        Falling,
        Raising,
        Flying
    }

    /// <summary>
    /// ����״̬
    /// </summary>
    private State state;

    /// <summary>
    /// �ṩ����������
    /// </summary>
    private int sunNum = 25;

    /// <summary>
    /// �����λ��
    /// </summary>
    private Vector3 raisePos;

    /// <summary>
    /// �����ٶ�
    /// </summary>
    private float raiseSpeed = 10f;

    /// <summary>
    /// ֹͣ����ĸ߶�
    /// </summary>
    private float stopFallingPosY;

    /// <summary>
    /// �����ٶ�
    /// </summary>
    private float fallSpeed;

    /// <summary>
    /// �����ٶ�
    /// </summary>
    public float flySpeed = 10f;

    /// <summary>
    /// ���ͣ��ʱ��
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
    /// ����
    /// </summary>
    private void Fall()
    {
        // ��δ���䵽ָ���߶ȣ������䣬������û���Update�ȴ�ָ��ʱ�����ʧ
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
    /// ����
    /// </summary>
    private void Raise()
    {
        // ��δ����ָ���߶ȣ���������𣬷������״̬Ϊ����
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
    /// ��UI��̫������
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
    /// ���������
    /// </summary>
    private void OnMouseDown()
    {
        state = State.Flying;
        GameController.Instance.SunNum += sunNum;
    }

    /// <summary>
    /// ������յ���������ʼ��
    /// </summary>
    /// <param name="posY">����ֹͣ�����λ��</param>
    public void SkySunInit(float posY)
    {
        stopFallingPosY = posY;
        fallSpeed = 1f;
        state = State.Falling;
    }

    /// <summary>
    /// ����ֲ�����������ʼ��
    /// </summary>
    /// <param name="posY">����ֹͣ�����λ��</param>
    public void PlantSunInit(Vector3 pos, float posY)
    {
        raisePos = pos;
        stopFallingPosY = posY;
        fallSpeed = 5f;
        state = State.Raising;
    }
}