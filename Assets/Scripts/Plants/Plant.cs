using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 植物
/// </summary>
public class Plant : MonoBehaviour
{
    /// <summary>
    /// 植物信息
    /// </summary>
    public PlantInfo plantInfo;

    /// <summary>
    /// 当前生命值
    /// </summary>
    public int currentHP;

    /// <summary>
    /// 所在网格
    /// </summary>
    public Grid grid;

    /// <summary>
    /// 自身的SpriteRenderer组件
    /// </summary>
    protected SpriteRenderer spriteRenderer;

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public virtual void Init(PlantInfo plantInfo, Grid grid)
    {
        this.plantInfo = plantInfo;
        this.grid = grid;
        currentHP = plantInfo.HP;
        spriteRenderer.material.color = Color.white;
    }

    /// <summary>
    /// 受到伤害
    /// </summary>
    /// <param name="damage">伤害值</param>
    public virtual void GetHurt(int damage)
    {
        if (currentHP <= 0) return;
        currentHP -= damage;
        StartCoroutine(Shine());
        if (currentHP <= 0)
        {
            SelfDestroy();
        }
    }

    protected IEnumerator Shine()
    {
        spriteRenderer.material.color = new Color(0.8f, 0.8f, 0.8f, 1);
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.material.color = Color.white;
    }

    protected void SelfDestroy()
    {
        StopAllCoroutines();
        PlantManager.Instance.RemovePlant(this);
    }
}