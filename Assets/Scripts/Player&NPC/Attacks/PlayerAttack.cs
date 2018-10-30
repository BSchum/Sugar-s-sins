using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerAttack : PlayerScript{
    Weapon weapon;
	// Use this for initialization
	public void Start () {
        Initialize();
        weapon = GetComponentInChildren<Weapon>();
	}
	
	// Update is called once per frame
	void Update () {
        if (ih.SimpleAttackInput())
        {
            Fire();
        }
	}
    public void Fire()
    {
        Ray r = new Ray(this.transform.position, this.transform.forward);
        RaycastHit rHit;
        if(Physics.Raycast(r, out rHit))
        {
            GameObject target = rHit.transform.gameObject;

            if(target.tag == Constants.ENEMY_TAG)
            {
                CmdAttack(target);
            }
        }
    }
    [Command]
    public void CmdAttack(GameObject target)
    {
        weapon = GetComponentInChildren<Weapon>();
        Debug.Log("Jattack "+target.name+" sur le server avec "+weapon.name);
        Health h = target.GetComponent<Health>();
        h.TakeDamage(weapon.damage);
    }


}
