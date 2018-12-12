using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HealSphere : NetworkBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        PlayerAttack player = other.GetComponent<PlayerAttack>();
        
        if (player != null)
        {
            HealPack healPack = transform.parent.GetComponent<HealPack>();
            healPack.DisableHealSphere(player.GetComponent<ObjectSpawnerController>());
        }
    }
}
