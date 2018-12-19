using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour {
    public int damage;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == Constants.ENEMY_TAG)
        {
            GetComponentInParent<GelatinSmashSkill>().OnSmashHit(damage, other);
        }
    }
}
