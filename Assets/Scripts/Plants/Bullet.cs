using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType
{
    Pea   //豌豆子弹
}

/// <summary>
/// 子弹
/// </summary>
public class Bullet : MonoBehaviour
{
    /// <summary>
    /// 移动速度
    /// </summary>
    protected float speed = 5.7f;

    /// <summary>
    /// 旋转速度
    /// </summary>
    protected float rotateSpeed = 360f;

    /// <summary>
    /// 伤害值
    /// </summary>
    public int damage = 20;

    /// <summary>
    /// 是否已击中
    /// </summary>
    protected bool isHit = false;

    /// <summary>
    /// 是否已击中
    /// </summary>
    public bool IsHit
    {
        get => isHit;
        set
        {
            isHit = value;
            if (isHit) StartCoroutine(Hit());
        }
    }

    /// <summary>
    /// 持续时间
    /// </summary>
    protected float duration = 0.1f;

    protected virtual void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
    }

    protected virtual IEnumerator Hit()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
