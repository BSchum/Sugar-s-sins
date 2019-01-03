using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HealSphere : NetworkBehaviour {

    HealPack healPack;

    private void Start()
    {
        healPack = transform.parent.GetComponent<HealPack>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerAttack player = other.GetComponent<PlayerAttack>();
        
        if (player != null)
        {
            
            healPack.DisableHealSphere();
        }
    }
}
