using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherry : Plant
{
    /// <summary>
    /// ��ը������
    /// </summary>
    private GameObject boomTrigger;

    protected override void Awake()
    {
        base.Awake();
        boomTrigger = transform.Find("BoomTrigger").gameObject;
    }

    public override void Init(PlantInfo plantInfo, Grid grid)
    {
        base.Init(plantInfo, grid);
        boomTrigger.SetActive(false);

        // ��ʼ��ը
        StartCoroutine(Boom());
    }

    private IEnumerator Boom()
    {
        GetComponent<Animator>().Play(0);
        yield return new WaitForSeconds(1f);
        boomTrigger.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        SelfDestroy();
    }
}
