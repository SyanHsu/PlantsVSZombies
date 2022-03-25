using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ֲ�����
/// </summary>
public class PlantManager : MonoBehaviour
{
    /// <summary>
    /// ����ģʽ
    /// </summary>
    public static PlantManager Instance;

    private PlantConf plantConf;

    /// <summary>
    /// ֲ���ֵ䣬ͨ��ֲ��������ֲ����Ϣ
    /// </summary>
    public Dictionary<PlantType, PlantInfo> plantDict;

    private void Awake()
    {
        Instance = this;
        plantConf = Resources.Load<PlantConf>("PlantConf");
        plantDict = new Dictionary<PlantType, PlantInfo>();
        CreateDict();
    }

    /// <summary>
    /// �����ֵ�
    /// </summary>
    private void CreateDict()
    {
        foreach (var item in plantConf.plantInfos)
        {
            plantDict.Add(item.plantType, item);
        }
    }
}
