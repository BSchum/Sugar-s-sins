using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CristalCone : MonoBehaviour {
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == Constants.PLAYER_TAG)
        {

            other.gameObject.GetComponent<Health>().TakeDamage(10);
            //root le joueur pendant x secondes
            other.gameObject.GetComponent<PlayerAttack>().AddBuff(new RootDebuff(other.gameObject));

            
        }
    }

}
