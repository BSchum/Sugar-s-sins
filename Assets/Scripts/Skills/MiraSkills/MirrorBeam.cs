using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MirrorBeam : NetworkBehaviour {
    Collider[] playersHitted;
    public float mirrorTick = 0.1f;
    public float currentCooldown = 0f;
    public float damage = 1;

    void FixedUpdate() {
        if(currentCooldown != 0f) {
            currentCooldown -= Time.deltaTime;
        }
    }
    
    void OnTriggerStay(Collider other)
    {
        if(other.tag == Constants.PLAYER_TAG && currentCooldown == 0f)
        {
            other.GetComponent<Health>().TakeDamage(damage);
            currentCooldown = mirrorTick;
        }
    }
}
