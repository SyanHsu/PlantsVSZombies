using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// 主界面UI管理器
/// </summary>
public class MainMenuUIManager : MonoBehaviour
{
    /// <summary>
    /// 关卡1按钮
    /// </summary>
    private Button round1Button;
    /// <summary>
    /// 关卡2按钮
    /// </summary>
    private Button round2Button;
    /// <summary>
    /// 退出按钮
    /// </summary>
    private Button exitButton;

    private void Awake()
    {
        round1Button = transform.Find("Button_Round1").GetComponent<Button>();
        round2Button = transform.Find("Button_Round2").GetComponent<Button>();
        exitButton = transform.Find("Button_Exit").GetComponent<Button>();
    }

    public void Round1ButtonOnClick()
    {
        CursorManager.Instance.SetCursorNormal();
        SceneManager.LoadScene("Scene_Round1");
    }

    public void Round2ButtonOnClick()
    {
        CursorManager.Instance.SetCursorNormal();
        SceneManager.LoadScene("Scene_Round2");
    }

    public void ExitButtonOnClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
