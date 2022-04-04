using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallNut : Plant
{
    /// <summary>
    /// 生命值阈值1
    /// </summary>
    private int thresholdHP1;
    /// <summary>
    /// 生命值阈值2
    /// </summary>
    private int thresholdHP2;

    private Animator animator;

    /// <summary>
    /// 闲置动画名称
    /// </summary>
    private string idleName = "WallNut_Idle";
    /// <summary>
    /// 状态1动画名称
    /// </summary>
    private string state1Name = "WallNut_State1";
    /// <summary>
    /// 状态2动画名称
    /// </summary>
    private string state2Name = "WallNut_State2";

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }

    public override void Init(PlantInfo plantInfo, Grid grid)
    {
        base.Init(plantInfo, grid);

        thresholdHP1 = plantInfo.HP * 2 / 3;
        thresholdHP2 = plantInfo.HP * 1 / 3;

        animator.Play(idleName);
    }

    /// <summary>
    /// 受到伤害
    /// </summary>
    /// <param name="damage">伤害值</param>
    public override void GetHurt(int damage)
    {
        if (currentHP <= 0) return;
        currentHP -= damage;
        StartCoroutine(Shine());
        if (currentHP <= 0)
        {
            SelfDestroy();
        }
        else if (currentHP <= thresholdHP2)
        {
            animator.Play(state2Name);
        }
        else if (currentHP <= thresholdHP1)
        {
            animator.Play(state1Name);
        }
    }
}
