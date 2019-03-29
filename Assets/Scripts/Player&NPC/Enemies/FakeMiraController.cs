using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeMiraController : EnemyController {
    public BossController mira;
    public GameObject hand;
    public static int aliveFakeMira;
	// Use this for initialization
	void Start () {
        base.Start();
        mira = GameObject.Find("Mira").GetComponent<BossController>();
	}
	
	// Update is called once per frame
	void Update () {
        base.Update();
        if(!skills[0].isOnCooldown && skills[0].HasRessource() && skills[0].CanCast())
        {
            Debug.Log("kuku");
            skills[0].source = this.gameObject;
            StartCoroutine(skills[0].Cast());
        }
    }

    private void OnDestroy()
    {
        aliveFakeMira -= 1;
    }
}
