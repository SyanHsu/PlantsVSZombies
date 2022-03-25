using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��Ϸ������
/// </summary>
public class GameController : MonoBehaviour
{
    /// <summary>
    /// ����ģʽ
    /// </summary>
    public static GameController Instance;

    /// <summary>
    /// ��Ϸ����
    /// </summary>
    public GameConf gameConf { get; private set; }

    private void Awake()
    {
        Instance = this;
        gameConf = Resources.Load<GameConf>("GameConf");
    }

    public void SetCursorLink()
    {
        Cursor.SetCursor(gameConf.Texture_LinkCursor, Vector2.zero, CursorMode.Auto);
    }

    public void SetCursorNormal()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
