using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerCharacter : NetworkBehaviour {

    [SerializeField]
    private GameObject heroPrefab;

    public Vector3 spawnPos;

    private void Start()
    {
        transform.position = spawnPos;
    }

    public override void OnStartLocalPlayer()
    {
        CmdRegister();
    }

    [Command]
    void CmdRegister ()
    {
       LobbyManager.singleton.SetPlayerPos(this.GetComponent<NetworkIdentity>());
    }
}
