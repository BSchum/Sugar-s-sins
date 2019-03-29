using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiraAnimations : MonoBehaviour {
    public Animator anim;
	

    public void Stay()
    {
        anim.SetBool("isWalking", false);
    }
    public void Walk()
    {
        anim.SetBool("isWalking", true);
    }
}
