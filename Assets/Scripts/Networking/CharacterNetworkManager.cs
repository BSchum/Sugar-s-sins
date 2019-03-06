using UnityEngine;
using UnityEngine.Networking;
class CharacterNetworkManager : NetworkManager
{
    static int nbPlayers;
    public GameObject tankPrefab;
    public GameObject magePrefab;
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        if(nbPlayers % 2 == 0)
        {
            playerPrefab = tankPrefab;
        }
        else {
            playerPrefab = magePrefab;
        }
        nbPlayers++;
        base.OnServerAddPlayer(conn, playerControllerId);
    }
}
