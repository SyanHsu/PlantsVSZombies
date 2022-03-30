using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 僵尸配置
/// </summary>
[CreateAssetMenu(fileName = "ZombieConf", menuName = "ZombieConf")]
public class ZombieConf : ScriptableObject
{
    /// <summary>
    /// 各种僵尸信息
    /// </summary>
    public ZombieInfo[] zombieInfos;
}
