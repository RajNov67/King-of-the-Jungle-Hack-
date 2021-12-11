using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI; 

public class RoomButton : MonoBehaviour
{
    [SerializeField]
    private Text nameText; //display for room name

    [SerializeField]
    private Text sizeText; //room size

    private string roomName;
    private int roomSize;
    private int playerCount;

    public void JoinRoomOnClick()
    {
        PhotonNetwork.JoinRoom(roomName); 
    }

    //public function called in Lobby Controller
    public void SetRoom(string nameInput, int sizeInput, int countInput)
    {
        roomName = nameInput;
        roomSize = sizeInput;
        playerCount = countInput;
        nameText.text = nameInput;
        sizeText.text = countInput + "/" + sizeInput; 
    }
}
