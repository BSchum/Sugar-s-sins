using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingDamage : MonoBehaviour {
    public Animator anim;
	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, anim.GetCurrentAnimatorClipInfo(0)[0].clip.length);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
