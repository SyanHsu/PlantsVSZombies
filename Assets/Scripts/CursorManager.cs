using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 鼠标指针控制器
/// </summary>
public class CursorManager : MonoBehaviour
{
    /// <summary>
    /// 单例模式
    /// </summary>
    public static CursorManager Instance;

    /// <summary>
    /// 点击时的鼠标指针图片
    /// </summary>
    public Texture2D linkCursorTexture;

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// 将鼠标指针改为点击链接
    /// </summary>
    public void SetCursorLink()
    {
        Cursor.SetCursor(linkCursorTexture, Vector2.zero, CursorMode.Auto);
    }

    /// <summary>
    /// 将鼠标指针设为默认
    /// </summary>
    public void SetCursorNormal()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
