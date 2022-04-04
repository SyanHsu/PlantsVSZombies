using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 僵尸管理
/// </summary>
public class ZombieManager : MonoBehaviour
{
    /// <summary>
    /// 单例模式
    /// </summary>
    public static ZombieManager Instance;

    /// <summary>
    /// 僵尸配置
    /// </summary>
    public ZombieConf zombieConf;

    /// <summary>
    /// 僵尸字典，通过僵尸种类找僵尸
    /// 信息
    /// </summary>
    public Dictionary<ZombieType, ZombieInfo> zombieDict;

    public List<Zombie>[] zombieLists;

    protected AudioSource audioSource;

    /// <summary>
    /// 僵尸的层级顺序
    /// </summary>
    private int[] zombieLayerOrder;
    /// <summary>
    /// 僵尸的最小层级顺序
    /// </summary>
    private int[] zombieMinLayerOrder;
    /// <summary>
    /// 僵尸的最大层级顺序
    /// </summary>
    private int[] zombieMaxLayerOrder;

    /// <summary>
    /// 创建僵尸的X轴坐标
    /// </summary>
    private float createPosX = 5.78f;
    /// <summary>
    /// 创建僵尸的Y轴坐标
    /// </summary>
    private float createPosY;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CreateRandomZombie();
        }
    }

    public void Init()
    {
        zombieConf = Resources.Load<ZombieConf>("ZombieConf");
        zombieDict = new Dictionary<ZombieType, ZombieInfo>();
        CreateDict();
        zombieLists = new List<Zombie>[GridManager.Instance.gridNumY];
        zombieLayerOrder = new int[GridManager.Instance.gridNumY];
        zombieMinLayerOrder = new int[GridManager.Instance.gridNumY];
        zombieMaxLayerOrder = new int[GridManager.Instance.gridNumY];
        for (int i = 0; i < GridManager.Instance.gridNumY; i++)
        {
            zombieLists[i] = new List<Zombie>();
            zombieLayerOrder[i] = zombieMinLayerOrder[i] = 100 * (GridManager.Instance.gridNumY - i - 1);
            zombieMaxLayerOrder[i] = 100 * (GridManager.Instance.gridNumY - i);
        }
        StartCoroutine(GenerateZombies());
    }

    /// <summary>
    /// 创建字典
    /// </summary>
    private void CreateDict()
    {
        foreach (var item in zombieConf.zombieInfos)
        {
            zombieDict.Add(item.zombieType, item);
        }
    }

    public void CreateRandomZombie()
    {
        int randomNum = Random.Range(0, 3);
        switch (randomNum)
        {
            case 0:
                CreateZombie(ZombieType.Zombie);
                break;
            case 1:
                CreateZombie(ZombieType.ConeheadZombie);
                break;
            case 2:
                CreateZombie(ZombieType.BucketheadZombie);
                break;
        }
    }

    public void CreateZombie(ZombieType zombieType)
    {
        // 僵尸生成的位置
        int row = Random.Range(0, GridManager.Instance.gridNumY);
        //int row = 2;
        createPosY = GridManager.Instance.GetPosY(row);
        Vector3 createPos = new Vector3(createPosX, createPosY);

        // 生成僵尸
        ZombieInfo zombieInfo = zombieDict[zombieType];
        Zombie createdZombie = PoolManager.Instance.GetGameObject(zombieInfo.prefab, 
            createPos, transform).GetComponent<Zombie>();
        if (zombieType == ZombieType.Zombie)
            createdZombie.Init(zombieInfo, row, zombieLayerOrder[row]++, Random.Range(1, 4));
        else createdZombie.Init(zombieInfo, row, zombieLayerOrder[row]++);
        if (zombieLayerOrder[row] == zombieMaxLayerOrder[row])
            zombieLayerOrder[row] = zombieMinLayerOrder[row];
        zombieLists[row].Add(createdZombie);
    }

    public void RemoveZombie(Zombie zombie)
    {
        zombieLists[zombie.row].Remove(zombie);
    }

    protected bool CheckZombie()
    {
        for (int i = 0; i < GridManager.Instance.gridNumY; i++)
        {
            if (zombieLists[i].Count > 0) return true;
        }
        return false;
    }

    protected virtual IEnumerator GenerateZombies()
    {
        float zombieInterval = 20f;
        yield return new WaitForSeconds(zombieInterval);
        audioSource.clip = GameController.Instance.audioClipConf.zombieComingClip;
        audioSource.Play();
        CreateZombie(ZombieType.Zombie);
        for (int i = 0; i < 2; i++)
        {
            yield return new WaitForSeconds(zombieInterval);
            CreateZombie(ZombieType.Zombie);
        }
        yield return new WaitForSeconds(zombieInterval);
        CreateZombie(ZombieType.Zombie);
        yield return new WaitForSeconds(1f);
        CreateZombie(ZombieType.Zombie);
        for (int i = 0; i < 2; i++)
        {
            yield return new WaitForSeconds(zombieInterval);
            CreateZombie(ZombieType.ConeheadZombie);
        }
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(zombieInterval);
            CreateZombie(ZombieType.ConeheadZombie);
            yield return new WaitForSeconds(1f);
            CreateZombie(ZombieType.Zombie);
        }
        yield return new WaitForSeconds(zombieInterval);
        GameObject finalWaveGO = GameController.Instance.transform.Find("FinalWave").gameObject;
        audioSource.clip = GameController.Instance.audioClipConf.hugeWaveClip;
        audioSource.Play();
        finalWaveGO.SetActive(true);
        finalWaveGO.GetComponent<Animator>().Play(0);
        yield return new WaitForSeconds(3f);
        finalWaveGO.SetActive(false);
        for (int i = 0; i < 2; i++)
        {
            CreateZombie(ZombieType.ConeheadZombie);
            yield return new WaitForSeconds(1f);
        }
        for (int i = 0; i < 7; i++)
        {
            CreateZombie(ZombieType.Zombie);
            yield return new WaitForSeconds(1f);
        }
        while (CheckZombie())
        {
            yield return new WaitForSeconds(0.1f);
        }
        GameController.Instance.WinGame();
    }
}