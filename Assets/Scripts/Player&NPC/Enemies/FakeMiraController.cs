using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeMiraController : EnemyController {

    public GameObject realMira;
	// Use this for initialization
	void Start () {
        base.Start();
        realMira = GameObject.Find("Mira");
	}
	
	// Update is called once per frame
	void Update () {
        base.Update();

    }
}
