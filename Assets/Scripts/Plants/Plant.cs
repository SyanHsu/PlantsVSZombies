using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ֲ��
/// </summary>
public class Plant : MonoBehaviour
{
    /// <summary>
    /// ����ֵ
    /// </summary>
    public int HP;

    /// <summary>
    /// ��������
    /// </summary>
    //public Grid grid;

    /// <summary>
    /// �ܵ��˺�
    /// </summary>
    /// <param name="damage">�˺�ֵ</param>
    public void GetHurt(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
