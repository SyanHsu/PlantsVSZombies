using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// ������UI������
/// </summary>
public class MainMenuUIManager : MonoBehaviour
{
    /// <summary>
    /// �ؿ�1��ť
    /// </summary>
    private Button round1Button;
    /// <summary>
    /// �ؿ�2��ť
    /// </summary>
    private Button round2Button;
    /// <summary>
    /// �˳���ť
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
