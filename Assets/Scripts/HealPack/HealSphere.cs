using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSphere : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        PlayerAttack player = other.GetComponent<PlayerAttack>();
        if(player != null)
        {
            //Si trigger un joueur, dis a son parent de le disable
            HealPack healPack = transform.parent.GetComponent<HealPack>();
            healPack.DisableHealSphere();
        }
    }
}
