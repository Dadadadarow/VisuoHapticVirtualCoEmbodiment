using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using UnityEngine;
using Mirror;
using Mirror.LiteNetLib4Mirror;

namespace CoEmbodimentSystem
{
    [RequireComponent(typeof(NetworkManager))]
    [RequireComponent(typeof(LiteNetLib4MirrorTransport))]
    [RequireComponent(typeof(LiteNetLib4MirrorDiscovery))]
    public class NetworkDiscoveryStarter : MonoBehaviour
    {
        [SerializeField] private bool _isHost;
        [SerializeField] private float _discoveryInterval = 1f;

        private bool _isDiscovering;
        private bool _isConnected;

        private NetworkManager _manager;

        void Awake()
        {
            _manager = GetComponent<NetworkManager>();
        }

        void Update()
        {
            if (_isConnected)
            {
                return;
            }

            if (_isHost)
            {
                if (!NetworkClient.isConnected && !NetworkServer.active)
                {
                    _manager.StartHost();
                }
            }
            else
            {
                StartCoroutine(StartDiscovery());
                _isConnected = true;
            }
        }

        private IEnumerator StartDiscovery()
        {
            _isDiscovering = true;

            LiteNetLib4MirrorDiscovery.InitializeFinder();
            LiteNetLib4MirrorDiscovery.Singleton.onDiscoveryResponse.AddListener(OnClientDiscoveryResponse);
            while(_isDiscovering)
            {
                LiteNetLib4MirrorDiscovery.SendDiscoveryRequest("BodyFusionNetwork");
                yield return new WaitForSeconds(_discoveryInterval);
            }

            LiteNetLib4MirrorDiscovery.Singleton.onDiscoveryResponse.RemoveListener(OnClientDiscoveryResponse);
            LiteNetLib4MirrorDiscovery.StopDiscovery();
        }

        private void OnClientDiscoveryResponse(IPEndPoint endPoint, string text)
        {
            string ip = endPoint.Address.ToString();

            NetworkManager.singleton.networkAddress = ip;
            NetworkManager.singleton.maxConnections = 2;
            LiteNetLib4MirrorTransport.Singleton.clientAddress = ip;
            LiteNetLib4MirrorTransport.Singleton.port = (ushort)endPoint.Port;
            LiteNetLib4MirrorTransport.Singleton.maxConnections = 2;
            NetworkManager.singleton.StartClient();

            _isDiscovering = false;
        }

        public void OnDisconnected()
        {
            _isConnected = false;
        }
    }
}