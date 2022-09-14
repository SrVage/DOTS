using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Code.Network
{
    public class InitialNetwork:MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private Transform[] _spawnPoints;
        private void Start()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            RoomOptions roomOptions = new RoomOptions()
            {
                IsVisible = false,
                MaxPlayers = 2
            };
            PhotonNetwork.JoinOrCreateRoom("my room", roomOptions, TypedLobby.Default);
        }

        public override void OnJoinedRoom()
        {
            var id = PhotonNetwork.LocalPlayer.ActorNumber;
            PhotonNetwork.Instantiate(_playerPrefab.name, _spawnPoints[id % _spawnPoints.Length].position, Quaternion.identity);
        }
    }
}