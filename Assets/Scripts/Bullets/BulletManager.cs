using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    /// <summary>
    /// ����ģʽ
    /// </summary>
    public static BulletManager Instance;

    private void Awake()
    {
        Instance = this;
    }
}
