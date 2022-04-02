using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
/// <summary>
/// �ӵ���Ϣ
/// </summary>
public class BulletInfo
{
    /// <summary>
    /// �ӵ�����
    /// </summary>
    public BulletType bulletType;

    /// <summary>
    /// �ӵ�Ԥ����
    /// </summary>
    public GameObject prefab;

    /// <summary>
    /// �ӵ�ͨ��ͼƬ
    /// </summary>
    public Sprite normalSprite;

    /// <summary>
    /// �ӵ�����ͼƬ
    /// </summary>
    public Sprite hitSprite;

    /// <summary>
    /// �ƶ��ٶ�
    /// </summary>
    public float speed;

    /// <summary>
    /// ��ת�ٶ�
    /// </summary>
    public float rotateSpeed;

    /// <summary>
    /// �˺�ֵ
    /// </summary>
    public int damage;
}
