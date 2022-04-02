using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �㶹����
/// </summary>
public class PeaShooter : Plant
{
    /// <summary>
    /// �㶹����״̬ö��
    /// </summary>
    private enum PeaShooterState
    {
        Idle,      // ����
        Shooting   // ���
    }

    /// <summary>
    /// �㶹����״̬
    /// </summary>
    private PeaShooterState state;

    /// <summary>
    /// �㶹����״̬
    /// </summary>
    private PeaShooterState State
    {
        get => state;
        set
        {
            state = value;
            // �����ÿ�Ƭ״̬ʱ������Ӧ��Э��
            switch(state)
            {
                case PeaShooterState.Idle:
                    StartCoroutine(Idle());
                    break;
                case PeaShooterState.Shooting:
                    StartCoroutine(Shoot());
                    break;
            }
        }
    }

    /// <summary>
    /// ������
    /// </summary>
    private float shootInterval = 1.4f;

    /// <summary>
    /// �ӵ�����
    /// </summary>
    private BulletInfo bulletInfo;

    /// <summary>
    /// �����
    /// </summary>
    private Transform firePoint;

    protected override void Awake()
    {
        base.Awake();
        firePoint = transform.Find("FirePoint");
    }

    public override void Init(PlantInfo plantInfo, Grid grid)
    {
        base.Init(plantInfo, grid);
        bulletInfo = PlantManager.Instance.bulletDict[BulletType.Pea];
        State = PeaShooterState.Idle;
    }

    /// <summary>
    /// ����
    /// </summary>
    /// <returns></returns>
    private IEnumerator Idle()
    {
        while (State == PeaShooterState.Idle)
        {
            yield return new WaitForSeconds(0.2f);
            if (CheckZombie()) State = PeaShooterState.Shooting;
        }
    }

    /// <summary>
    /// ���
    /// </summary>
    /// <returns></returns>
    private IEnumerator Shoot()
    {
        while (State == PeaShooterState.Shooting)
        {
            yield return new WaitForSeconds(shootInterval);
            if (!CheckZombie())
            {
                State = PeaShooterState.Idle;
                break;
            }
            Bullet bullet = PoolManager.Instance.GetGameObject(bulletInfo.prefab, 
                firePoint.position, BulletManager.Instance.transform).GetComponent<Bullet>();
            bullet.Init(bulletInfo);
        }
    }

    /// <summary>
    /// �ж�ͬһ���Ƿ��н�ʬ
    /// </summary>
    /// <returns>ͬһ���Ƿ��н�ʬ</returns>
    private bool CheckZombie()
    {
        foreach (var item in ZombieManager.Instance.zombieLists[(int)grid.point.y])
        {
            if (item.transform.position.x >= grid.leftPosX)
                return true;
        }
        return false;
    }
}