using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LobbyManager : NetworkBehaviour {

    [SerializeField]
    private Transform[] spawnPos;

    public static LobbyManager singleton;

    private void Start()
    {
        if(LobbyManager.singleton == null)
            LobbyManager.singleton = this;
    }

    public void SetPlayerPos(NetworkIdentity player)
    {
        player.transform.position = LobbyManager.singleton.spawnPos[CharacterNetworkManager.nbPlayers - 1].position;
        RpcSetPlayerPos(player.transform.position, player);
    }

    [ClientRpc]
    public void RpcSetPlayerPos (Vector3 newPos, NetworkIdentity player)
    {
        player.transform.position = newPos;
    }
}
