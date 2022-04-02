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
    private enum GameState
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
    private GameState State
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
    /// UI���
    /// </summary>
    private GameObject UIPanel;

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
    /// ׼���׶�
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
    /// ��Ϸ��ʼ
    /// </summary>
    /// <returns></returns>
    private void StartGame()
    {
        UIPanel.SetActive(true);
        UIManager.Instance.Init(plantTypes);
        SkySunManager.Instance.Init();
    }

    /// <summary>
    /// ��Ϸ����
    /// </summary>
    /// <returns></returns>
    private IEnumerator GameOver()
    {
        yield return StartCoroutine(CameraMove.Instance.Move(leftCameraPosX));
    }
}
