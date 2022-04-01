using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Íã¶¹×Óµ¯
/// </summary>
public class Pea : Bullet
{
    protected override void Update()
    {
        if (IsHit) return;
        base.Update();
        transform.Rotate(Vector3.back * rotateSpeed * Time.deltaTime);
    }

    protected override IEnumerator Hit()
    {
        GetComponent<SpriteRenderer>().sprite = PlantManager.Instance.plantConf.peaHitSprite;
        GetComponent<Rigidbody2D>().gravityScale = 10;
        yield return base.Hit();
    }
}
