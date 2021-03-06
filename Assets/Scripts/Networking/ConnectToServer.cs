using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//Give access to multiplayer tools of Photon
using Photon.Pun;
using Photon.Realtime; 

using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        SceneManager.LoadScene("Lobby");
    }


}
