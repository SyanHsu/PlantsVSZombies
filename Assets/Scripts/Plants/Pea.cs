using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �㶹�ӵ�
/// </summary>
public class Pea : MonoBehaviour
{
    /// <summary>
    /// �ƶ��ٶ�
    /// </summary>
    private float speed = 5.7f;

    /// <summary>
    /// �˺�ֵ
    /// </summary>
    public int damage = 20;

    private void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
