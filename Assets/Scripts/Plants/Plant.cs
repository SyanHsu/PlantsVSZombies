using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 植物
/// </summary>
public class Plant : MonoBehaviour
{
    /// <summary>
    /// 生命值
    /// </summary>
    public int HP;

    /// <summary>
    /// 所在网格
    /// </summary>
    //public Grid grid;

    /// <summary>
    /// 受到伤害
    /// </summary>
    /// <param name="damage">伤害值</param>
    public void GetHurt(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
