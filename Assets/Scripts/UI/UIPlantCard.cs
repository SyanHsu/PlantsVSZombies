using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// UIֲ�￨Ƭ
/// </summary>
public class UIPlantCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    /// <summary>
    /// ��Ƭ״̬ö��
    /// </summary>
    private enum CardState
    {
        CDing,       // ��ȴ��
        LackofSun,   // ȱ������
        Plantable,   // ����ֲ
        Planting     // ��ֲ��
    }

    /// <summary>
    /// ��Ƭ״̬
    /// </summary>
    private CardState state;

    /// <summary>
    /// ��Ƭ״̬
    /// </summary>
    private CardState State
    {
        get => state;
        set
        {
            state = value;
            // �����ÿ�Ƭ״̬ʱ������Ӧ��Э��
            switch (state)
            {
                case CardState.CDing:
                    StartCoroutine(EnterCD());
                    break;
                case CardState.LackofSun:
                    StartCoroutine(WaitingForSun());
                    break;
                case CardState.Plantable:
                    StartCoroutine(WaitingToBePlanted());
                    break;
                case CardState.Planting:
                    StartCoroutine(Planting());
                    break;
            }
        }
    }

    /// <summary>
    /// ��ͼƬ���
    /// </summary>
    private Image grayImage;
    /// <summary>
    /// ��ͼƬ���
    /// </summary>
    private Image darkImage;
    /// <summary>
    /// ��ͼƬ���
    /// </summary>
    private Image brightImage;
    /// <summary>
    /// �����ı�
    /// </summary>
    private GameObject explGO;

    /// <summary>
    /// ֲ������
    /// </summary>
    public PlantType plantType;

    /// <summary>
    /// ֲ������
    /// </summary>
    public PlantInfo plantInfo;

    /// <summary>
    /// չʾ������Ϣ
    /// </summary>
    private bool showExpl;

    /// <summary>
    /// ��갴ť����
    /// </summary>
    private int mouseButtonPressed;

    private void Awake()
    {
        // �õ���Ӧ��ͼƬ���
        Image[] images = GetComponentsInChildren<Image>();
        foreach (var item in images)
        {
            if (item.name.EndsWith("Gray"))
            {
                grayImage = item;
            }
            else if (item.name.EndsWith("Dark"))
            {
                darkImage = item;
            }
            else if (item.name.EndsWith("Bright"))
            {
                brightImage = item;
            }
        }
        explGO = transform.Find("Explaination").gameObject;
    }

    public void Init(PlantType plantType = PlantType.Default)
    {
        this.plantType = plantType;
        if (plantType == PlantType.Default) return;

        // �õ���Ӧ��ֲ����Ϣ
        plantInfo = PlantManager.Instance.plantDict[plantType];
        grayImage.sprite = darkImage.sprite = brightImage.sprite = plantInfo.card;

        // ��ͼƬ��������߼��Ŀ����Ϊtrue
        grayImage.raycastTarget = true;

        // ��ʼ����Ƭ״̬Ϊ����ֲ�Ļ�CD��
        if (plantInfo.CDTime == 7.5f) State = CardState.LackofSun;
        else State = CardState.CDing;
    }

    /// <summary>
    /// ����CD
    /// </summary>
    /// <returns></returns>
    private IEnumerator EnterCD()
    {
        // ��ʾ��ͼƬ
        brightImage.enabled = false;
        darkImage.fillAmount = 0;
        
        // ���ϸ���CD��
        float currentCDTime = 0;
        float deltaFillAmount = 0.1f / plantInfo.CDTime;
        while (currentCDTime < plantInfo.CDTime)
        {
            yield return new WaitForSeconds(0.1f);
            darkImage.fillAmount += deltaFillAmount;
            currentCDTime += 0.1f;
        }

        // �����ⲻ�������״̬Ϊȱ�����⣬�������Ϊ����ֲ
        if (PlayerStatus.Instance.SunNum < plantInfo.neededSun) State = CardState.LackofSun;
        else State = CardState.Plantable;
    }

    /// <summary>
    /// �ȴ�����
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitingForSun()
    {
        // ��ʾ��ͼƬ
        brightImage.enabled = false;
        darkImage.fillAmount = 1;
        
        //�ȴ��������㹻
        while (PlayerStatus.Instance.SunNum < plantInfo.neededSun)
        {
            yield return 0;
        }

        // �����㹻������״̬Ϊ����ֲ
        State = CardState.Plantable;
    }

    /// <summary>
    /// �ȴ�����ֲ
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitingToBePlanted()
    {
        // ��ʾ��ͼƬ
        brightImage.enabled = true;
        darkImage.fillAmount = 0;
        
        while (State == CardState.Plantable)
        {
            if (PlayerStatus.Instance.SunNum < plantInfo.neededSun) State = CardState.LackofSun;
            yield return 0;
        }
    }

    /// <summary>
    /// ��ֲ��
    /// </summary>
    /// <returns></returns>
    private IEnumerator Planting()
    {
        // ��ʾ��ͼƬ
        brightImage.enabled = false;
        darkImage.fillAmount = 0;

        // ��������ͼƬ
        GameObject plantImage = PoolManager.Instance.GetGameObject(plantInfo.image, Vector3.zero, PlantManager.Instance.transform);
        plantImage.GetComponent<SpriteRenderer>().material.color = Color.white;
        plantImage.GetComponent<SpriteRenderer>().sortingOrder = 1;
        // Ҫ��ֲ������İ�͸��ͼƬ
        GameObject plantTranslucentImage = PoolManager.Instance.GetGameObject(plantInfo.image, Vector3.zero, PlantManager.Instance.transform);
        plantTranslucentImage.GetComponent<SpriteRenderer>().material.color = new Color(1, 1, 1, 0.6f);
        plantImage.GetComponent<SpriteRenderer>().sortingOrder = 0;

        // �������״̬
        PlayerStatus.Instance.state = PlayerStatus.PlayerState.Planting;

        // �õ����λ��
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // ͼƬ������ƫ����
        float deltaX = 0.08f;
        float deltaY = 0.32f;

        // Ҫ��ֲ������
        Grid grid;

        while (State == CardState.Planting)
        {
            // ��ȡ����������������
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            plantImage.transform.position = new Vector3(mousePos.x + deltaX, mousePos.y + deltaY);
            grid = GridManager.Instance.GetNearestGrid(mousePos);

            // �������������������ʾ��͸��ͼƬ��������ʾ
            if (grid == null || grid.planted)
            {
                plantTranslucentImage.SetActive(false);
            }
            else
            {
                plantTranslucentImage.SetActive(true);
                plantTranslucentImage.transform.position = grid.pos;
            }

            if (mouseButtonPressed != -1)
            {
                if (Input.GetMouseButtonUp(mouseButtonPressed))
                {
                    mouseButtonPressed = -1;
                }
            }
            //����������ʱ���������������ֲֲ�����ָ�����ֲ״̬
            else if (Input.GetMouseButtonDown(0))
            {
                PoolManager.Instance.PushGameObject(plantImage, plantInfo.image);
                PoolManager.Instance.PushGameObject(plantTranslucentImage, plantInfo.image);
                if (grid == null || grid.planted)
                {
                    State = CardState.Plantable;
                }
                else
                {
                    PlantManager.Instance.Plant(plantInfo, grid);
                    State = CardState.CDing;
                }
            }
            // ��������Ҽ�ʱ���ָ�����ֲ״̬
            else if (Input.GetMouseButtonDown(1))
            {
                PoolManager.Instance.PushGameObject(plantImage, plantInfo.image);
                PoolManager.Instance.PushGameObject(plantTranslucentImage, plantInfo.image);
                State = CardState.Plantable;
            }
            yield return 0;
        }

        // �Ļ����״̬
        PlayerStatus.Instance.state = PlayerStatus.PlayerState.Default;
    }

    /// <summary>
    /// ������
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        // ��ʾ������Ϣ
        showExpl = true;

        StartCoroutine(PointerStay());
    }

    private IEnumerator PointerStay()
    {
        while (showExpl)
        {
            if (PlayerStatus.Instance.state != PlayerStatus.PlayerState.Planting)
            {
                // ��ʾ�������ֺͱ���
                explGO.SetActive(true);
                // ��Ӧ��Ƭ��ͬ״̬�������ֺͱ�����С
                switch (state)
                {
                    case CardState.CDing:
                        explGO.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(
                            RectTransform.Axis.Horizontal, 90);
                        explGO.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(
                            RectTransform.Axis.Vertical, 34);
                        explGO.GetComponentInChildren<Text>().text =
                            "<color=red>����װ����...</color>\n" + plantInfo.name;
                        break;
                    case CardState.LackofSun:
                        explGO.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(
                            RectTransform.Axis.Horizontal, 105);
                        explGO.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(
                            RectTransform.Axis.Vertical, 34);
                        explGO.GetComponentInChildren<Text>().text =
                            "<color=red>û���㹻������</color>\n" + plantInfo.name;
                        break;
                    case CardState.Plantable:
                        explGO.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(
                            RectTransform.Axis.Horizontal, 15 * plantInfo.name.Length);
                        explGO.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(
                            RectTransform.Axis.Vertical, 17);
                        explGO.GetComponentInChildren<Text>().text = plantInfo.name;
                        // �������ָ��
                        CursorManager.Instance.SetCursorLink();
                        break;
                }
            }
            yield return 0;
        }

        // ȡ����ʾ�������ֺͱ���
        explGO.SetActive(false);

        // �������ָ��
        CursorManager.Instance.SetCursorNormal();
    }

    /// <summary>
    /// ����˳�
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        // ����ʾ������Ϣ
        showExpl = false;
    }

    /// <summary>
    /// �����
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        if (PlayerStatus.Instance.state == PlayerStatus.PlayerState.Planting) return;

        if (State != CardState.Plantable) return;

        mouseButtonPressed = (int)eventData.button;
        State = CardState.Planting;
    }
}