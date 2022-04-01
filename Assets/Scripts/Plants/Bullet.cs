using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType
{
    Pea   //�㶹�ӵ�
}

/// <summary>
/// �ӵ�
/// </summary>
public class Bullet : MonoBehaviour
{
    /// <summary>
    /// �ƶ��ٶ�
    /// </summary>
    protected float speed = 5.7f;

    /// <summary>
    /// ��ת�ٶ�
    /// </summary>
    protected float rotateSpeed = 360f;

    /// <summary>
    /// �˺�ֵ
    /// </summary>
    public int damage = 20;

    /// <summary>
    /// �Ƿ��ѻ���
    /// </summary>
    protected bool isHit = false;

    /// <summary>
    /// �Ƿ��ѻ���
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
    /// ����ʱ��
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
