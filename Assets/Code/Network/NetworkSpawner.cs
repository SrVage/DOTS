using Photon.Pun;
using UnityEngine;

namespace Code.Network
{
    public class NetworkSpawner:MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameObject _prefab;

        public override void OnJoinedRoom()
        {
            if (PhotonNetwork.LocalPlayer.ActorNumber>1)
                return;
            PhotonNetwork.Instantiate(_prefab.name, transform.position, transform.rotation);
        }
    }
}