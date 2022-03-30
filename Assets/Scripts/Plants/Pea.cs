using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 豌豆子弹
/// </summary>
public class Pea : MonoBehaviour
{
    /// <summary>
    /// 移动速度
    /// </summary>
    private float speed = 5.7f;

    /// <summary>
    /// 伤害值
    /// </summary>
    public int damage = 20;

    private void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
