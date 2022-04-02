using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSunManager : MonoBehaviour
{
    /// <summary>
    /// µ¥ÀýÄ£Ê½
    /// </summary>
    public static PlantSunManager Instance;

    private void Awake()
    {
        Instance = this;
    }
}
