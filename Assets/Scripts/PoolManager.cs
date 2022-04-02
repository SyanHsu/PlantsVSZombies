using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 对象池管理
/// </summary>
public class PoolManager
{
    /// <summary>
    /// 单例模式
    /// </summary>
    private static PoolManager instance;
    public static PoolManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PoolManager();
            }
            return instance;
        }
    }

    /// <summary>
    /// 对象池字典
    /// </summary>
    private Dictionary<GameObject, List<GameObject>> poolDict = new Dictionary<GameObject, List<GameObject>>();

    /// <summary>
    /// 对象池实例对象
    /// </summary>
    private GameObject poolObj = new GameObject("PoolObj");

    /// <summary>
    /// 获取对象
    /// </summary>
    /// <param name="prefab">对象预制体</param>
    /// <returns>对象</returns>
    public GameObject GetGameObject(GameObject prefab, Vector3 position, Transform parent)
    {
        GameObject obj;
        if (poolDict.ContainsKey(prefab) && poolDict[prefab].Count > 0)
        {
            obj = poolDict[prefab][0];
            poolDict[prefab].RemoveAt(0);
            obj.SetActive(true);
            obj.transform.position = position;
            obj.transform.SetParent(parent);
        }
        else
        {
            obj = Object.Instantiate<GameObject>(prefab, position, Quaternion.identity, parent);
        }
        return obj;
    }

    /// <summary>
    /// 把对象放进对象池中
    /// </summary>
    /// <param name="gameObject">对象</param>
    /// <param name="prefab">对象预制体</param>
    public void PushGameObject(GameObject gameObject, GameObject prefab)
    {
        if (poolDict.ContainsKey(prefab))
        {
            poolDict[prefab].Add(gameObject);
        }
        else
        {
            poolDict.Add(prefab, new List<GameObject> { gameObject });
            new GameObject(prefab.name).transform.SetParent(poolObj.transform);
        }
        gameObject.SetActive(false);
        gameObject.transform.SetParent(poolObj.transform.Find(prefab.name));
    }
}
