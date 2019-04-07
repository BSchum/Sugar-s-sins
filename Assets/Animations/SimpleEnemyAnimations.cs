using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyAnimations : BaseAnimation {

    public void Attack()
    {
        anim.SetBool("isAttacking", true);
        anim.SetBool("isWalking", false);
    }

    public override void Stay()
    {
        anim.SetBool("isAttacking", false);
        anim.SetBool("isWalking", false);
        base.Stay();
    }

    public override void Walk()
    {
        anim.SetBool("isAttacking", false);
        base.Walk();
    }
}
