using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Mirror.LiteNetLib4Mirror;

namespace CoEmbodimentSystem
{
    public class BodyFusionNetworkManager : LiteNetLib4MirrorNetworkManager
    {
        [SerializeField] private Transform _spawnTransform;

        public override void OnServerAddPlayer(NetworkConnection conn)
        {
            GameObject player = Instantiate(playerPrefab, _spawnTransform.position, _spawnTransform.rotation);
            NetworkServer.AddPlayerForConnection(conn, player);
        }

        public override void OnServerDisconnect(NetworkConnection conn)
        {
            // add some lines if you'd like this manager to do something before it destroys the player object

            base.OnServerDisconnect(conn);
        }
    }
}