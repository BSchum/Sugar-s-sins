using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CristalProjectile : SkillProjectile {
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.LookAt(target.transform);
        if ((transform.position - target.transform.position).magnitude > 0.5)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else
        {
            target.GetComponent<Health>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }
}
