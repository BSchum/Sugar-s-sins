using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

class CharacterNetworkManager : NetworkManager
{
    public static int nbPlayers;
    public GameObject tankPrefab;
    public GameObject magePrefab;

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        nbPlayers++;
        base.OnServerAddPlayer(conn, playerControllerId);
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        nbPlayers--;
        base.OnServerDisconnect(conn);
    }
}