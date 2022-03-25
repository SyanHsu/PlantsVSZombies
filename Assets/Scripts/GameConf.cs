using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏配置
/// </summary>
[CreateAssetMenu(fileName = "GameConf", menuName = "GameConf")]
public class GameConf : ScriptableObject
{
    /// <summary>
    /// 阳光预制体
    /// </summary>
    public GameObject Prefab_Sun;

    /// <summary>
    /// 点击时的鼠标指针预制体
    /// </summary>
    public Texture2D Texture_LinkCursor;
}
