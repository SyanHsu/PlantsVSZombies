using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquashTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Zombie" || collision.tag == "BucketheadZombie")
        {
            transform.parent.GetComponent<Squash>().Attack(collision.transform.position);
        }
    }
}
