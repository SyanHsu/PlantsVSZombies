using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 子弹
/// </summary>
public class Bullet : MonoBehaviour
{
    /// <summary>
    /// 子弹信息
    /// </summary>
    public BulletInfo bulletInfo;

    /// <summary>
    /// 是否已击中
    /// </summary>
    protected bool isHit;

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

    public virtual void Init(BulletInfo bulletInfo)
    {
        GetComponent<BoxCollider2D>().enabled = true;
        this.bulletInfo = bulletInfo;
        IsHit = false;
    }

    protected virtual void Update()
    {
        transform.Translate(Vector3.right * bulletInfo.speed * Time.deltaTime, Space.World);
    }

    protected virtual IEnumerator Hit()
    {
        yield return new WaitForSeconds(0.1f);
        SelfDestroy();
    }

    protected void SelfDestroy()
    {
        StopAllCoroutines();
        PoolManager.Instance.PushGameObject(gameObject, bulletInfo.prefab);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsHit) return;
        switch (collision.tag)
        {
            case "Zombie":
                collision.GetComponent<Zombie>().GetHurt(bulletInfo.damage);
                GetComponent<BoxCollider2D>().enabled = false;
                IsHit = true;
                transform.position = collision.GetComponent<Zombie>().bodyCenterTransform.position;
                break;
            case "RightBoundary":
            case "LeftBoundary":
                SelfDestroy();
                break;
        }
    }
}
