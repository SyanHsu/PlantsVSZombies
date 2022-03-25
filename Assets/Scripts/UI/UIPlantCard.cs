using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// UIֲ�￨Ƭ
/// </summary>
public class UIPlantCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
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
    public PlantType plantType = PlantType.SunFlower;

    /// <summary>
    /// ֲ������
    /// </summary>
    public PlantInfo plantInfo;

    private void Start()
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

        // �õ���Ӧ��ֲ����Ϣ
        plantInfo = PlantManager.Instance.plantDict[plantType];

        // ��ʼ����Ƭ״̬ΪCD��
        State = CardState.CDing;
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
        GameObject plantImage = Instantiate<GameObject>(plantInfo.image, Vector3.zero, 
            Quaternion.identity, PlantManager.Instance.transform);
        // Ҫ��ֲ������İ�͸��ͼƬ
        GameObject plantTranslucentImage = null;

        // �������״̬
        PlayerStatus.Instance.state = PlayerStatus.PlayerState.Planting;

        // �õ����λ��
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // ͼƬ������ƫ����
        float deltaX = 0.08f;
        float deltaY = 0.32f;

        Grid grid = null;
        Grid currentGrid;

        while (State == CardState.Planting)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            plantImage.transform.position = new Vector3(mousePos.x + deltaX, mousePos.y + deltaY);
            currentGrid = GridManager.Instance.GetNearestGrid(mousePos);
            if (currentGrid != grid)
            {
                Destroy(plantTranslucentImage);
                grid = currentGrid;
                if (currentGrid != null && !currentGrid.planted)
                {
                    plantTranslucentImage = Instantiate<GameObject>(plantInfo.translucentImage, 
                        currentGrid.pos, Quaternion.identity, PlantManager.Instance.transform);
                }
            }
            if (Input.GetMouseButtonDown(0))
            {
                Destroy(plantTranslucentImage);
                Destroy(plantImage);
                if (grid == null || grid.planted)
                {
                    State = CardState.Plantable;
                }
                else
                {
                    GameObject plant = Instantiate<GameObject>(plantInfo.prefab, grid.pos,
                        Quaternion.identity, PlantManager.Instance.transform);
                    grid.planted = true;
                    PlayerStatus.Instance.SunNum -= plantInfo.neededSun;
                    State = CardState.CDing;
                }
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
        if (PlayerStatus.Instance.state == PlayerStatus.PlayerState.Planting) return;

        // ��ʾ�������ֺͱ���
        explGO.SetActive(true);

        // ��Ӧ��Ƭ��ͬ״̬�������ֺͱ�����С
        switch (state)
        {
            case CardState.CDing:
                explGO.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 90);
                explGO.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 34);
                explGO.GetComponentInChildren<Text>().text = 
                    "<color=red>����װ����...</color>\n" + brightImage.sprite.name;
                break;
            case CardState.LackofSun:
                explGO.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 105);
                explGO.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 34);
                explGO.GetComponentInChildren<Text>().text = 
                    "<color=red>û���㹻������</color>\n" + brightImage.sprite.name;
                break;
            case CardState.Plantable:
                explGO.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 15 * brightImage.sprite.name.Length);
                explGO.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 17);
                explGO.GetComponentInChildren<Text>().text = brightImage.sprite.name;

                // �������ָ��
                GameController.Instance.SetCursorLink();
                break;
        }
    }

    /// <summary>
    /// ����˳�
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        if (PlayerStatus.Instance.state == PlayerStatus.PlayerState.Planting) return;

        // ȡ����ʾ�������ֺͱ���
        explGO.SetActive(false);

        // �������ָ��
        GameController.Instance.SetCursorNormal();
    }

    /// <summary>
    /// �����
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (PlayerStatus.Instance.state == PlayerStatus.PlayerState.Planting) return;

        if (State != CardState.Plantable) return;

        State = CardState.Planting;

        // ȡ����ʾ�������ֺͱ���
        explGO.SetActive(false);

        // �������ָ��
        GameController.Instance.SetCursorNormal();
    }
}
