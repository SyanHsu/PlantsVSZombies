using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ʬ����
/// </summary>
[CreateAssetMenu(fileName = "ZombieConf", menuName = "ZombieConf")]
public class ZombieConf : ScriptableObject
{
    /// <summary>
    /// ���ֽ�ʬ��Ϣ
    /// </summary>
    public ZombieInfo[] zombieInfos;
}
