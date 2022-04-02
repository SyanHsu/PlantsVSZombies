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
    private enum GameState
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
    private GameState State
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
    /// UI面板
    /// </summary>
    private GameObject UIPanel;

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

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UIPanel = UIManager.Instance.gameObject;
        plantTypes = new PlantType[UIManager.Instance.cardNum];
        State = GameState.Ready;
    }

    /// <summary>
    /// 准备阶段
    /// </summary>
    /// <returns></returns>
    private IEnumerator Ready()
    {
        UIPanel.SetActive(false);
        CameraMove.Instance.transform.position = new Vector3
            (leftCameraPosX, 0, CameraMove.Instance.transform.position.z);
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(CameraMove.Instance.Move(rightCameraPosX));

        yield return new WaitForSeconds(1f);

        plantTypes[0] = PlantType.SunFlower;
        plantTypes[1] = PlantType.PeaShooter;

        yield return StartCoroutine(CameraMove.Instance.Move(gamingCameraPosX));
        yield return new WaitForSeconds(1f);

        State = GameState.Gaming;
    }

    /// <summary>
    /// 游戏开始
    /// </summary>
    /// <returns></returns>
    private void StartGame()
    {
        UIPanel.SetActive(true);
        UIManager.Instance.Init(plantTypes);
        SkySunManager.Instance.Init();
    }

    /// <summary>
    /// 游戏结束
    /// </summary>
    /// <returns></returns>
    private IEnumerator GameOver()
    {
        yield return StartCoroutine(CameraMove.Instance.Move(leftCameraPosX));
    }
}
