using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class PlayersAnimations : BaseAnimation
{
    public void StrafeLeft()
    {
        anim.SetBool("isStrafingLeft", true);
        anim.SetBool("isStrafingRight", false);
        anim.SetBool("isWalking", false);

    }

    public void StrafeRight()
    {
        anim.SetBool("isStrafingRight", true);
        anim.SetBool("isStrafingLeft", false);
        anim.SetBool("isWalking", false);
    }

    public override void Stay()
    {
        base.Stay();
        anim.SetBool("isStrafingLeft", false);
        anim.SetBool("isStrafingRight", false);
        anim.SetBool("isWalking", false);
    }

    public override void Walk()
    {
        base.Walk();
        anim.SetBool("isStrafingLeft", false);
        anim.SetBool("isStrafingRight", false);
    }


}

