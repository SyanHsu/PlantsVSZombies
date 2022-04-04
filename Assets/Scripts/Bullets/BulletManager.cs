using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    /// <summary>
    /// µ¥ÀýÄ£Ê½
    /// </summary>
    public static BulletManager Instance;

    public AudioSource audioSource;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }
}
