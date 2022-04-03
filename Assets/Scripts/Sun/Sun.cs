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
                    StartCoroutine(Waiting());
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
    private float fallSpeed;

    /// <summary>
    /// ���ָ���Ƿ񱻸ı�
    /// </summary>
    public bool cursorChanged;

    /// <summary>
    /// �����SpriteRenderer���
    /// </summary>
    private SpriteRenderer sunSprite;

    private void Awake()
    {
        sunSprite = GetComponent<SpriteRenderer>();
    }

    private void Init(float posY)
    {
        sunSprite.sortingOrder = sunSortingLayer++;
        if (sunSortingLayer == 100) sunSortingLayer = 0;
        sunSprite.material.color = Color.white;
        transform.localScale = Vector3.one;
        cursorChanged = false;
        stopFallingPosY = posY;
    }

    /// <summary>
    /// ������յ���������ʼ��
    /// </summary>
    /// <param name="posY">����ֹͣ�����λ��</param>
    public void SkySunInit(float posY)
    {
        // ���ò���
        Init(posY);
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
        // ���ò���
        Init(posY);
        raisePos = pos;
        fallSpeed = 8f;

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
        // �����ٶ�
        float raiseSpeed = 10f;

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
        float flySpeed = 20f;
        float leftDist;
        // ��δ��UI�����������
        while (transform.position.y < UIManager.Instance.UISunPosition.y)
        {
            leftDist = Vector3.Distance(transform.position, UIManager.Instance.UISunPosition);
            if (leftDist < 1f)
            {
                sunSprite.material.color = new Color(1, 1, 1, 0.4f + leftDist / 2);
                transform.localScale = new Vector3(0.5f + leftDist / 2, 0.5f + leftDist / 2);
            }
            transform.Translate(Vector3.Normalize(UIManager.Instance.UISunPosition - transform.position) * flySpeed * Time.deltaTime);
            yield return 0;
        } ;

        // ��Ϸ��̫��������
        PlayerStatus.Instance.SunNum += sunNum;

        // ��������
        SelfDestroy();
    }

    /// <summary>
    /// ������
    /// </summary>
    private void OnMouseEnter()
    {
        if (!CursorManager.Instance.changable || Time.timeScale == 0) return;

        // �������ָ��
        CursorManager.Instance.SetCursorLink();
        cursorChanged = true;
    }

    /// <summary>
    /// ����˳�
    /// </summary>
    private void OnMouseExit()
    {
        if (!CursorManager.Instance.changable || Time.timeScale == 0) return;

        // �������ָ��
        CursorManager.Instance.SetCursorNormal();
        cursorChanged = false;
    }

    /// <summary>
    /// �����
    /// </summary>
    private void OnMouseDown()
    {
        if (!CursorManager.Instance.changable || Time.timeScale == 0) return;

        // ״̬����Ϊ����
        State = SunState.Flying;
    }

    private IEnumerator Waiting()
    {
        yield return new WaitForSeconds(maxStayTime);
        SelfDestroy();
    }

    private void SelfDestroy()
    {
        if (cursorChanged)
        {
            CursorManager.Instance.SetCursorNormal();
        }
        StopAllCoroutines();
        PoolManager.Instance.PushGameObject(gameObject, PlantManager.Instance.plantConf.sunPrefab);
    }
}