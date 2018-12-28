using UnityEngine;
using UnityEngine.Networking;
class CharacterNetworkManager : NetworkManager
{
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        playerPrefab = CharaterManager.choosedCharacter;
        base.OnServerAddPlayer(conn, playerControllerId);
    }
}
