using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    /// <summary>
    /// µ¥ÀýÄ£Ê½
    /// </summary>
    public static BulletManager Instance;

    private void Awake()
    {
        Instance = this;
    }
}
