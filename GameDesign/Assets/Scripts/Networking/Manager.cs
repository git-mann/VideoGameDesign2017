using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class Manager : NetworkManager {
public override void OnServerAddPlayer(NetworkConnection conn, short PlayerControllerId)
    {
        GameObject player = GameObject.Instantiate(playerPrefab, playerPrefab.transform.position, playerPrefab.transform.rotation);
        player.name = "ship" + PlayerControllerId;
        NetworkServer.AddPlayerForConnection(conn, player, PlayerControllerId);
        Debug.Log("PLayer Id is: " + PlayerControllerId);
    }
}
