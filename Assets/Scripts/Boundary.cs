using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �߽�
/// </summary>
public class Boundary : MonoBehaviour
{
    /// <summary>
    /// ���봥����
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            Destroy(collision.gameObject);
        }    
    }
}
