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
            if (state == PeaShooterState.Shooting)
                StartCoroutine(Shoot());
        }
    }

    /// <summary>
    /// ������
    /// </summary>
    private float shootInterval = 1.4f;

    private void Update()
    {
        
    }

    private IEnumerator Shoot()
    {
        while (State == PeaShooterState.Shooting)
        {
            yield return new WaitForSeconds(shootInterval);
        }
    }
}
