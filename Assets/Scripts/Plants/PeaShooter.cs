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
    /// �����
    /// </summary>
    private Transform firePoint;

    private void Start()
    {
        firePoint = transform.Find("FirePoint");
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
            yield return new WaitForSeconds(0.1f);
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
            Instantiate<GameObject>(PlantManager.Instance.plantConf.peaPrefab, 
                firePoint.position, Quaternion.identity, transform);
            if (!CheckZombie()) State = PeaShooterState.Idle;
        }
    }

    /// <summary>
    /// �ж�ͬһ���Ƿ��н�ʬ
    /// </summary>
    /// <returns>ͬһ���Ƿ��н�ʬ</returns>
    private bool CheckZombie()
    {
        return true;
    }
}
