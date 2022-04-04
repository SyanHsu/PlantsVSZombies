using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeheadZombie : Zombie
{
    private int thresholdHP = 270;

    private bool withConehead;

    public override void Init(ZombieInfo zombieInfo, int row, int sortingOrder, int walkIndex = 0)
    {
        withConehead = true;
        walkName = "ConeheadZombie_Walk";
        attackName = "ConeheadZombie_Attack";
        base.Init(zombieInfo, row, sortingOrder);
    }

    /// <summary>
    ///  ‹µΩ…À∫¶
    /// </summary>
    /// <param name="damage">…À∫¶÷µ</param>
    public override void GetHurt(int damage)
    {
        if (currentHP <= 0) return;
        currentHP -= damage;
        StartCoroutine(Shine());
        if (currentHP <= thresholdHP && withConehead)
        {
            walkName = "Zombie_Walk1";
            attackName = "Zombie_Attack";
            if (State == ZombieState.Walking)
            {
                bodyAnimator.Play(walkName, 0, bodyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime);
            }
            else if (State == ZombieState.Attacking)
            {
                bodyAnimator.Play(attackName, 0, bodyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime);
            }
            withConehead = false;
        }
        if (currentHP <= zombieInfo.dieHP && State != ZombieState.Dead)
            State = ZombieState.Dead;
    }
}
