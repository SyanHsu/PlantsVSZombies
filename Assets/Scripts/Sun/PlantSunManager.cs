using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSunManager : MonoBehaviour
{
    /// <summary>
    /// ����ģʽ
    /// </summary>
    public static PlantSunManager Instance;

    private void Awake()
    {
        Instance = this;
    }
}
