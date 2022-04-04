using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squash : Plant
{
    /// <summary>
    /// Ì½²â´¥·¢Æ÷
    /// </summary>
    private GameObject squashTrigger;
    /// <summary>
    /// ¹¥»÷´¥·¢Æ÷
    /// </summary>
    private GameObject squashAttackTrigger;
    
    private Animator animator;

    private string idleName = "Squash_Default";
    private string attackName = "Squash_Attack";

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        squashTrigger = transform.Find("SquashTrigger").gameObject;
        squashAttackTrigger = transform.Find("SquashAttack").gameObject;
    }

    public override void Init(PlantInfo plantInfo, Grid grid)
    {
        base.Init(plantInfo, grid);
        GetComponent<BoxCollider2D>().enabled = true;
        squashTrigger.SetActive(true);
        squashAttackTrigger.SetActive(false);
        GetComponent<Animator>().speed = 1f;
        animator.Play(idleName);
    }

    public void Attack(Vector3 attackPos)
    {
        StartCoroutine(DoAttack(attackPos));
    }

    private IEnumerator DoAttack(Vector3 attackPos)
    {
        squashTrigger.SetActive(false);
        GetComponent<BoxCollider2D>().enabled = false;

        GetComponent<Animator>().speed = 0;
        GetComponent<Animator>().Play(attackName);


        Vector3 startPos = transform.position;
        float timer = 0;
        while (timer <= 0.5f)
        {
            transform.position = Vector3.Lerp(startPos, attackPos, timer);
            timer += Time.deltaTime;
            yield return 0;
        }
        GetComponent<Animator>().speed = 1f;

        squashAttackTrigger.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        SelfDestroy();
    }
}
