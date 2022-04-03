using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    /// <summary>
    /// ����ģʽ
    /// </summary>
    public static GameController Instance;

    /// <summary>
    /// ��Ϸ״̬ö��
    /// </summary>
    public enum GameState
    {
        Ready,     // ׼��
        Gaming,    // ��Ϸ��
        GameOver   // ��Ϸ����
    }

    /// <summary>
    /// ��Ϸ״̬
    /// </summary>
    private GameState state;

    /// <summary>
    /// ��Ϸ״̬
    /// </summary>
    public GameState State
    {
        get => state;
        set
        {
            state = value;
            // ��������Ϸ״̬ʱ������Ӧ��Э��
            switch (state)
            {
                case GameState.Ready:
                    StartCoroutine(Ready());
                    break;
                case GameState.Gaming:
                    StartGame();
                    break;
                case GameState.GameOver:
                    StartCoroutine(GameOver());
                    break;
            }
        }
    }

    /// <summary>
    /// ��Ϸ�е�ֲ������
    /// </summary>
    private PlantType[] plantTypes;

    /// <summary>
    /// ����˵������x��λ��
    /// </summary>
    private float leftCameraPosX = -4.7f;
    /// <summary>
    /// ���Ҷ˵������x��λ��
    /// </summary>
    private float rightCameraPosX = 4.7f;
    /// <summary>
    /// ��Ϸ�е������x��λ��
    /// </summary>
    private float gamingCameraPosX = -1.4f;

    /// <summary>
    /// ��ʬչʾ
    /// </summary>
    private GameObject showZombies;
    /// <summary>
    /// ׼������ֲ�����������
    /// </summary>
    private GameObject preparePlantGO;
    /// <summary>
    /// ��ʬ�Ե����ӵ�ͼƬ����
    /// </summary>
    private SpriteRenderer brainLostBGSprite;
    /// <summary>
    /// ��ʬ�Ե����ӵ�ͼƬ
    /// </summary>
    private Transform brainLost;

    private void Awake()
    {
        Instance = this;
        showZombies = transform.Find("ShowZombies").gameObject;
        preparePlantGO = transform.Find("PreparePlant").gameObject;
        brainLostBGSprite = transform.Find("BrainLostBG").GetComponent<SpriteRenderer>();
        brainLost = transform.Find("BrainLostBG/BrainLost");
    }

    private void Start()
    {
        plantTypes = new PlantType[UIManager.Instance.cardNum];

        // ״̬��ʼΪ׼��
        State = GameState.Ready;
    }

    /// <summary>
    /// ׼���׶�
    /// </summary>
    /// <returns></returns>
    private IEnumerator Ready()
    {
        UIManager.Instance.mainPanelGO.SetActive(false);
        CameraMove.Instance.transform.position = new Vector3
            (leftCameraPosX, 0, CameraMove.Instance.transform.position.z);
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(CameraMove.Instance.Move(rightCameraPosX));

        yield return new WaitForSeconds(1f);

        plantTypes[0] = PlantType.SunFlower;
        plantTypes[1] = PlantType.PeaShooter;

        yield return StartCoroutine(CameraMove.Instance.Move(gamingCameraPosX));
        preparePlantGO.SetActive(true);
        preparePlantGO.GetComponent<Animator>().Play(0);
        yield return new WaitForSeconds(3f);
        preparePlantGO.SetActive(false);

        showZombies.SetActive(false);
        State = GameState.Gaming;
    }

    /// <summary>
    /// ��Ϸ��ʼ
    /// </summary>
    /// <returns></returns>
    private void StartGame()
    {
        UIManager.Instance.mainPanelGO.SetActive(true);
        UIManager.Instance.Init(plantTypes);
        PlayerStatus.Instance.SunNum = 50;
        PoolManager.Instance = new PoolManager();
        SkySunManager.Instance.Init();
        ZombieManager.Instance.Init();
    }

    /// <summary>
    /// ��Ϸ����
    /// </summary>
    /// <returns></returns>
    private IEnumerator GameOver()
    {
        UIManager.Instance.mainPanelGO.SetActive(false);
        Time.timeScale = 0;
        yield return StartCoroutine(CameraMove.Instance.Move(leftCameraPosX));
        yield return new WaitForSecondsRealtime(1f);
        brainLostBGSprite.gameObject.SetActive(true);
        brainLostBGSprite.material.color = Color.clear;
        brainLost.localScale = Vector3.zero;
        Color aimColor = new Color(0f, 0f, 0f, 0.8f);
        Vector3 aimScale = new Vector3(0.0724f, 0.1f, 1f);
        float timer = 0f;
        while (timer < 1f)
        {
            timer += Time.unscaledDeltaTime;
            brainLostBGSprite.material.color = Color.Lerp(Color.clear, aimColor, timer);
            brainLost.localScale = Vector3.Lerp(Vector3.zero, aimScale, timer);
            yield return 0;
        }
        yield return new WaitForSecondsRealtime(1f);
        brainLostBGSprite.gameObject.SetActive(false);
        UIManager.Instance.diePanelGO.SetActive(true);
    }

    public void WinGame()
    {
        UIManager.Instance.winPanelGO.SetActive(true);
    }
}
