using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ָ�������
/// </summary>
public class CursorManager : MonoBehaviour
{
    /// <summary>
    /// ����ģʽ
    /// </summary>
    public static CursorManager Instance;

    /// <summary>
    /// ���ʱ�����ָ��ͼƬ
    /// </summary>
    public Texture2D linkCursorTexture;

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// �����ָ���Ϊ�������
    /// </summary>
    public void SetCursorLink()
    {
        Cursor.SetCursor(linkCursorTexture, Vector2.zero, CursorMode.Auto);
    }

    /// <summary>
    /// �����ָ����ΪĬ��
    /// </summary>
    public void SetCursorNormal()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
