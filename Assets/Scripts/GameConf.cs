using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��Ϸ����
/// </summary>
[CreateAssetMenu(fileName = "GameConf", menuName = "GameConf")]
public class GameConf : ScriptableObject
{
    /// <summary>
    /// ����Ԥ����
    /// </summary>
    public GameObject Prefab_Sun;

    /// <summary>
    /// ���ʱ�����ָ��Ԥ����
    /// </summary>
    public Texture2D Texture_LinkCursor;
}
