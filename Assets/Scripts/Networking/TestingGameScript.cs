using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//Give access to multiplayer tools of Photon
using Photon.Pun;
using Photon.Realtime;

using UnityEngine.SceneManagement;

public class TestingGameScript : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    private string room = "testRoom";

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(room);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(room);

    }

}
