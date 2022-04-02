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

    /// <summary>
    /// �ӵ��ֵ䣬ͨ���ӵ��������ӵ���Ϣ
    /// </summary>
    public Dictionary<BulletType, BulletInfo> bulletDict;

    private void Awake()
    {
        Instance = this;
        plantConf = Resources.Load<PlantConf>("PlantConf");
        plantDict = new Dictionary<PlantType, PlantInfo>();
        bulletDict = new Dictionary<BulletType, BulletInfo>();
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
        foreach (var item in plantConf.bulletInfos)
        {
            bulletDict.Add(item.bulletType, item);
        }
    }

    /// <summary>
    /// ��ֲֲ��
    /// </summary>
    public void Plant(PlantInfo plantInfo, Grid grid)
    {
        GameObject plant = PoolManager.Instance.GetGameObject(plantInfo.prefab, grid.pos, transform);
        plant.GetComponent<Plant>().Init(plantInfo, grid);
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
        PoolManager.Instance.PushGameObject(plant.gameObject, plant.plantInfo.prefab);
    }
}
