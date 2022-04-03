using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// ���ӹ���
/// </summary>
public class ShovelManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    /// <summary>
    /// ��������ʹ��
    /// </summary>
    private bool isUsing;

    /// <summary>
    /// ��������ʹ��
    /// </summary>
    private bool IsUsing
    {
        get => isUsing;
        set
        {
            isUsing = value;
            // �����ÿ�Ƭ״̬ʱ������Ӧ��Э��
            if (isUsing) StartCoroutine(UseShovel());
        }
    }

    private GameObject shovelGO;

    /// <summary>
    /// ��갴ť����
    /// </summary>
    private int mouseButtonPressed;

    private void Awake()
    {
        // �õ���Ӧ�Ĳ������
        shovelGO = transform.Find("Shovel").gameObject;
        IsUsing = false;
    }


    /// <summary>
    /// ʹ�ò���
    /// </summary>
    /// <returns></returns>
    private IEnumerator UseShovel()
    {
        // ԭ���Ӳ��ɼ�
        shovelGO.SetActive(false);

        // ��������ͼƬ
        GameObject shovelImage = PoolManager.Instance.GetGameObject(PlantManager.Instance.plantConf.shovelImage, Vector3.zero, PlantManager.Instance.transform);
        shovelImage.transform.rotation = Quaternion.Euler(0, 0, 45);
        // ���ָ�벻�ɸı�
        CursorManager.Instance.changable = false;

        // �õ����λ��
        Vector3 mousePos;

        // ͼƬ������ƫ����
        float deltaX = 0.5f;
        float deltaY = 0.45f;

        // Ҫ����������
        Grid grid;

        while (IsUsing)
        {
            // ��ȡ����������������
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            shovelImage.transform.position = new Vector3(mousePos.x + deltaX, mousePos.y + deltaY);
            grid = GridManager.Instance.GetNearestGrid(mousePos);

            if (mouseButtonPressed != -1)
            {
                if (Input.GetMouseButtonUp(mouseButtonPressed))
                {
                    mouseButtonPressed = -1;
                }
            }
            //����������ʱ����������������ֲ�����ָ�����״̬����������Ҽ�ʱ���ָ�����״̬
            else if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                if (Input.GetMouseButtonDown(0) && grid != null && grid.planted)
                {
                    PlantManager.Instance.RemovePlant(grid.plantedPlant);
                    
                }
                PoolManager.Instance.PushGameObject(shovelImage, PlantManager.Instance.plantConf.shovelImage);
                IsUsing = false;
            }
            yield return 0;
        }

        // �Ļ����״̬
        CursorManager.Instance.changable = true;

        // �ָ�����
        shovelGO.SetActive(true);
    }

    /// <summary>
    /// ������
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!CursorManager.Instance.changable || Time.timeScale == 0) return;

        CursorManager.Instance.SetCursorLink();
    }

    /// <summary>
    /// ����˳�
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        if (Time.timeScale == 0) return;

        CursorManager.Instance.SetCursorNormal();
    }

    /// <summary>
    /// �����
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!CursorManager.Instance.changable || Time.timeScale == 0) return;

        mouseButtonPressed = (int)eventData.button;
        IsUsing = true;
    }
}