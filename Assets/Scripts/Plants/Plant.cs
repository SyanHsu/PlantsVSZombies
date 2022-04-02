using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ֲ��
/// </summary>
public class Plant : MonoBehaviour
{
    /// <summary>
    /// ֲ����Ϣ
    /// </summary>
    public PlantInfo plantInfo;

    /// <summary>
    /// ��ǰ����ֵ
    /// </summary>
    public int currentHP;

    /// <summary>
    /// ��������
    /// </summary>
    public Grid grid;

    /// <summary>
    /// �����SpriteRenderer���
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
    /// �ܵ��˺�
    /// </summary>
    /// <param name="damage">�˺�ֵ</param>
    public void GetHurt(int damage)
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

    private void SelfDestroy()
    {
        StopAllCoroutines();
        PlantManager.Instance.RemovePlant(this);
    }
}