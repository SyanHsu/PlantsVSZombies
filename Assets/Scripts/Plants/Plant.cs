using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ֲ��
/// </summary>
public class Plant : MonoBehaviour
{
    /// <summary>
    /// ����ֵ
    /// </summary>
    public int HP;

    /// <summary>
    /// ��������
    /// </summary>
    public Grid grid;

    /// <summary>
    /// �����SpriteRenderer���
    /// </summary>
    protected SpriteRenderer spriteRenderer;

    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// �ܵ��˺�
    /// </summary>
    /// <param name="damage">�˺�ֵ</param>
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
