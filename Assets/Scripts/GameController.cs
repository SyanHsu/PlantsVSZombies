using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    /// <summary>
    /// 单例模式
    /// </summary>
    public static GameController Instance;

    /// <summary>
    /// 游戏状态枚举
    /// </summary>
    public enum GameState
    {
        Ready,     // 准备
        Gaming,    // 游戏中
        GameOver   // 游戏结束
    }

    /// <summary>
    /// 游戏状态
    /// </summary>
    private GameState state;

    /// <summary>
    /// 游戏状态
    /// </summary>
    public GameState State
    {
        get => state;
        set
        {
            state = value;
            // 当设置游戏状态时调用相应的协程
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
    /// 游戏中的植物种类
    /// </summary>
    private PlantType[] plantTypes;

    /// <summary>
    /// 最左端的摄像机x轴位置
    /// </summary>
    private float leftCameraPosX = -4.7f;
    /// <summary>
    /// 最右端的摄像机x轴位置
    /// </summary>
    private float rightCameraPosX = 4.7f;
    /// <summary>
    /// 游戏中的摄像机x轴位置
    /// </summary>
    private float gamingCameraPosX = -1.4f;

    /// <summary>
    /// 僵尸展示
    /// </summary>
    private GameObject showZombies;
    /// <summary>
    /// 准备安放植物的文字物体
    /// </summary>
    private GameObject preparePlantGO;
    /// <summary>
    /// 僵尸吃掉脑子的图片背景
    /// </summary>
    private SpriteRenderer brainLostBGSprite;
    /// <summary>
    /// 僵尸吃掉脑子的图片
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

        // 状态初始为准备
        State = GameState.Ready;
    }

    /// <summary>
    /// 准备阶段
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
    /// 游戏开始
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
    /// 游戏结束
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
