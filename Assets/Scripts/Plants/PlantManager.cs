using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 植物管理
/// </summary>
public class PlantManager : MonoBehaviour
{
    /// <summary>
    /// 单例模式
    /// </summary>
    public static PlantManager Instance;

    /// <summary>
    /// 植物配置
    /// </summary>
    public PlantConf plantConf;

    /// <summary>
    /// 植物字典，通过植物种类找植物信息
    /// </summary>
    public Dictionary<PlantType, PlantInfo> plantDict;

    /// <summary>
    /// 子弹字典，通过子弹种类找子弹信息
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
    /// 创建字典
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
    /// 种植植物
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
    /// 去除植物
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
