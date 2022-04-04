using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquashAttackTrigger : MonoBehaviour
{
    private int damage = 2000;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Zombie" || collision.tag == "BucketheadZombie")
        {
            collision.GetComponent<Zombie>().dieMode = 2;
            collision.GetComponent<Zombie>().GetHurt(damage);
        }
    }
}
