using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 边界
/// </summary>
public class Boundary : MonoBehaviour
{
    /// <summary>
    /// 进入触发器
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            Destroy(collision.gameObject);
        }    
    }
}
