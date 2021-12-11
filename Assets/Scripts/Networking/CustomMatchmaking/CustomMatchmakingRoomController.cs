using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class CustomMatchmakingRoomController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private string mapName; //scene index for loading multiplayer scene

    [SerializeField]
    private GameObject lobbyPanel; //display when in lobby
    [SerializeField]
    private GameObject roomPanel; //display when in room

    [SerializeField]
    private GameObject startButton; //for master client. Used to start game and load scenes

    [SerializeField]
    private Transform playersContainer; //used to display all the players in the current room
    [SerializeField]
    private GameObject playerListingPrefab; //instantiate to display each player in room

    [SerializeField]
    private Text roomNameDisplay; //display name of the room

    void ClearPlayerListings()
    {
        for (int i = playersContainer.childCount - 1; i >= 0; i--)
        {
            Destroy(playersContainer.GetChild(i).gameObject);
        }
    }
    void listPlayers()
    {
        foreach (Player player in PhotonNetwork.PlayerList) //loops thorugh players, instantiating them into the playersContainer
        {
            GameObject tempListing = Instantiate(playerListingPrefab, playersContainer);
            Text tempText = tempListing.transform.GetChild(0).GetComponent<Text>();
            tempText.text = player.NickName;
        }
    }

    public override void OnJoinedRoom() //called when local player joins room
    {
        roomPanel.SetActive(true); //activate display for being in a room
        lobbyPanel.SetActive(false); //hide lobby display
        roomNameDisplay.text = PhotonNetwork.CurrentRoom.Name;

        if (PhotonNetwork.IsMasterClient) //if client is master
        {
            startButton.SetActive(true); 
        }
        else
        {
            startButton.SetActive(false);
        }
        //remove old player listings and relist all current players
        ClearPlayerListings();
        listPlayers();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer) //called whenever a new player joins a room
    {
        ClearPlayerListings();
        listPlayers();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        ClearPlayerListings();
        listPlayers();

        if (PhotonNetwork.IsMasterClient) //if local player is now the master client
        {
            startButton.SetActive(true);
        }
    }

    public void StartGame() //paired to start button. Load players into multiplayer session
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false; //This means that once the game has started, no new players can join the game. If we decide to allow this functionality remove this line
            PhotonNetwork.LoadLevel(mapName);
        }
    }

    IEnumerator rejoinLobby()
    {
        yield return new WaitForSeconds(1);
        PhotonNetwork.JoinLobby(); 
    }

    public void BackOnClick()
    {
        lobbyPanel.SetActive(true);
        roomPanel.SetActive(false);
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LeaveRoom();
        StartCoroutine(rejoinLobby()); //solves master client error. This will leave and rejoin the lobby.
    }
}
