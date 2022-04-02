using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Íã¶¹×Óµ¯
/// </summary>
public class Pea : Bullet
{

    public override void Init(BulletInfo bulletInfo)
    {
        base.Init(bulletInfo);
        GetComponent<SpriteRenderer>().sprite = bulletInfo.normalSprite;
        GetComponent<Rigidbody2D>().gravityScale = 0;
    }


    protected override void Update()
    {
        if (IsHit) return;
        base.Update();
        transform.Rotate(Vector3.back * bulletInfo.rotateSpeed * Time.deltaTime);
    }

    protected override IEnumerator Hit()
    {
        GetComponent<SpriteRenderer>().sprite = bulletInfo.hitSprite;
        GetComponent<Rigidbody2D>().gravityScale = 10;
        yield return StartCoroutine(base.Hit());
    }
}
