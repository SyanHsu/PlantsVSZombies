using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����ع���
/// </summary>
public class PoolManager
{
    /// <summary>
    /// ����ģʽ
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
    /// ������ֵ�
    /// </summary>
    private Dictionary<GameObject, List<GameObject>> poolDict = new Dictionary<GameObject, List<GameObject>>();

    /// <summary>
    /// �����ʵ������
    /// </summary>
    private GameObject poolObj = new GameObject("PoolObj");

    /// <summary>
    /// ��ȡ����
    /// </summary>
    /// <param name="prefab">����Ԥ����</param>
    /// <returns>����</returns>
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
    /// �Ѷ���Ž��������
    /// </summary>
    /// <param name="gameObject">����</param>
    /// <param name="prefab">����Ԥ����</param>
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
