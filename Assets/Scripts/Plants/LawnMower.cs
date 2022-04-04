using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 小推车
/// </summary>
public class LawnMower : MonoBehaviour
{
    /// <summary>
    /// 是否已触发
    /// </summary>
    private bool isHit = false;

    /// <summary>
    /// 速度
    /// </summary>
    private float speed = 5.7f;

    /// <summary>
    /// 伤害值
    /// </summary>
    private int damage = 2000;

    private IEnumerator Move()
    {
        while(true)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            yield return 0;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Zombie":
            case "BucketheadZombie":
                collision.GetComponent<Zombie>().dieMode = 2;
                collision.GetComponent<Zombie>().GetHurt(damage);
                if (!isHit)
                {
                    isHit = true;
                    GetComponent<AudioSource>().clip = GameController.Instance.audioClipConf.LawnMowerClip;
                    GetComponent<AudioSource>().Play();
                    StartCoroutine(Move());
                }
                break;
            case "RightBoundary":
                Destroy(gameObject);
                break;
        }
    }

}
