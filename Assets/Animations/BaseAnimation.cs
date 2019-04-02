using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAnimation : MonoBehaviour {
    public Animator anim;
	

    public virtual void Stay()
    {
        anim.SetBool("isWalking", false);
    }
    public virtual void Walk()
    {
        anim.SetBool("isWalking", true);
    }
}
