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
    public Grid grid;

    /// <summary>
    /// 自身的SpriteRenderer组件
    /// </summary>
    protected SpriteRenderer spriteRenderer;

    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// 受到伤害
    /// </summary>
    /// <param name="damage">伤害值</param>
    public void GetHurt(int damage)
    {
        HP -= damage;
        StartCoroutine(Shine());
        if (HP <= 0)
        {
            PlantManager.Instance.RemovePlant(this);
        }
    }

    protected IEnumerator Shine()
    {
        spriteRenderer.material.color = new Color(0.8f, 0.8f, 0.8f, 1);
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.material.color = Color.white;
    }
}
