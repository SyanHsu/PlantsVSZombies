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
    /// ���ͣ��ʱ��
    /// </summary>
    private float maxStayTime = 7f;

    /// <summary>
    /// ����״̬ö��
    /// </summary>
    private enum SunState
    {
        Idle,     // ����
        Falling,  // ����
        Raising,  // ���𣨶�ֲ����������⣩
        Flying    // ���У���UI��
    }

    /// <summary>
    /// ����״̬
    /// </summary>
    private SunState state;

    /// <summary>
    /// ����״̬
    /// </summary>
    private SunState State
    {
        get => state;
        set
        {
            state = value;
            // ����������״̬ʱ������Ӧ��Э��
            switch (state)
            {
                case SunState.Idle:
                    // һ��ʱ�����������
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
    /// �ṩ����������
    /// </summary>
    private int sunNum = 25;

    /// <summary>
    /// �����λ��
    /// </summary>
    private Vector3 raisePos;

    /// <summary>
    /// ֹͣ����ĸ߶�
    /// </summary>
    private float stopFallingPosY;

    /// <summary>
    /// �����ٶ�
    /// </summary>
    private float raiseSpeed = 10f;

    /// <summary>
    /// �����ٶ�
    /// </summary>
    private float fallSpeed;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sortingOrder = sunSortingLayer++;
    }

    /// <summary>
    /// ������յ���������ʼ��
    /// </summary>
    /// <param name="posY">����ֹͣ�����λ��</param>
    public void SkySunInit(float posY)
    {
        // ��������
        stopFallingPosY = posY;
        fallSpeed = 1f;

        // ��ʼ״̬Ϊ����
        State = SunState.Falling;
    }

    /// <summary>
    /// ����ֲ�����������ʼ��
    /// </summary>
    /// <param name="posY">����ֹͣ�����λ��</param>
    public void PlantSunInit(Vector3 pos, float posY)
    {
        // ��������
        raisePos = pos;
        stopFallingPosY = posY;
        fallSpeed = 5f;

        // ��ʼ״̬Ϊ����
        State = SunState.Raising;
    }

    /// <summary>
    /// ����
    /// </summary>
    private IEnumerator Fall()
    {
        // ��δ���䵽ָ���߶ȣ�������
        while (transform.position.y > stopFallingPosY)
        {
            transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
            yield return 0;
        }

        // ����״̬Ϊ����
        State = SunState.Idle;
    }

    /// <summary>
    /// ����
    /// </summary>
    private IEnumerator Raise()
    {
        // ��δ����ָ���߶ȣ����������
        while (Vector3.Distance(transform.position, raisePos) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, raisePos,
                raiseSpeed * Time.deltaTime);
            yield return 0;
        }

        // ����״̬Ϊ��·
        State = SunState.Falling;
    }

    /// <summary>
    /// ��UI��̫������
    /// </summary>
    private IEnumerator Fly()
    {
        float flySpeed = 8f;
        SpriteRenderer sunSprite = GetComponent<SpriteRenderer>();
        Color transColor = new Color(1, 1, 1, 0);
        float leftDist;
        // ��δ��UI�����������
        do
        {
            transform.position = Vector3.Lerp(transform.position, UIManager.Instance.UISunPosition,
                flySpeed * Time.deltaTime);
            leftDist = Vector3.Distance(transform.position, UIManager.Instance.UISunPosition);
            if (leftDist < 0.5f) sunSprite.material.color = new Color(1, 1, 1, leftDist + 0.4f);
            yield return 0;
        } while (leftDist > 0.2f);

        // ��������
        Destroy(gameObject);
    }

    /// <summary>
    /// ������
    /// </summary>
    private void OnMouseEnter()
    {
        if (PlayerStatus.Instance.state == PlayerStatus.PlayerState.Planting) return;

        // �������ָ��
        GameController.Instance.SetCursorLink();
    }

    /// <summary>
    /// ����˳�
    /// </summary>
    private void OnMouseExit()
    {
        if (PlayerStatus.Instance.state == PlayerStatus.PlayerState.Planting) return;

        // �������ָ��
        GameController.Instance.SetCursorNormal();
    }

    /// <summary>
    /// �����
    /// </summary>
    private void OnMouseDown()
    {
        if (PlayerStatus.Instance.state == PlayerStatus.PlayerState.Planting) return;

        // ״̬����Ϊ����
        State = SunState.Flying;

        // ��Ϸ��̫��������
        PlayerStatus.Instance.SunNum += sunNum;
    }
}