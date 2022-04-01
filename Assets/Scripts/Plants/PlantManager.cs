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

    /// <summary>
    /// ֲ������
    /// </summary>
    public PlantConf plantConf;

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

    /// <summary>
    /// ��ֲֲ��
    /// </summary>
    public void Plant(PlantInfo plantInfo, Grid grid)
    {
        GameObject plant = Instantiate<GameObject>(plantInfo.prefab, grid.pos,
                        Quaternion.identity, transform);
        plant.GetComponent<Plant>().HP = plantInfo.HP;
        plant.GetComponent<Plant>().grid = grid;
        grid.planted = true;
        grid.plantedPlant = plant.GetComponent<Plant>();
        PlayerStatus.Instance.SunNum -= plantInfo.neededSun;
    }

    /// <summary>
    /// ȥ��ֲ��
    /// </summary>
    public void RemovePlant(Plant plant)
    {
        if(plant.grid != null)
        {
            plant.grid.planted = false;
            plant.grid.plantedPlant = null;
        }
        Destroy(plant.gameObject);
    }
}
