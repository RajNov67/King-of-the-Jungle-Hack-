using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class CustomMatchmakingLobbyController : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private GameObject lobbyConnectButton; //Button used to join a lobby
    [SerializeField]
    private GameObject lobbyPanel; //panel to display lobby
    [SerializeField]
    private GameObject mainPanel; // panel to display main menu

    public InputField playerNameInput; //Player can change their nickname

    private string roomName; //save room name
    private int roomSize = 2; //~~~~~ player cap set to 2 initial release ~~~~~

    private List<RoomInfo> roomListings; //list of rooms

    [SerializeField]
    private Transform roomsContainer; //container to hold all rooms
    [SerializeField]
    private GameObject roomListingPrefab; //prefab to display each room in lobby

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true; //master client scene is synced to all clients
        lobbyConnectButton.SetActive(true);//activate button to connect to lobby otherwise show loading button
        roomListings = new List<RoomInfo>(); // initiliase roomListing


        //check if player name saved to player preferences

        if (PlayerPrefs.HasKey("NickName"))
        {
            if (PlayerPrefs.GetString("NickName") == "")
            {
                PhotonNetwork.NickName = "Player " + Random.Range(0, 1000);
            }
            else
            {
                PhotonNetwork.NickName = PlayerPrefs.GetString("NickName");
            }
            playerNameInput.text = PhotonNetwork.NickName;
        }

    }

    public void PlayerNameUpdate(string nameInput) //input for player name assinged to player pref
    {
        PhotonNetwork.NickName = nameInput;
        PlayerPrefs.SetString("NickName", nameInput);
        playerNameInput.text = nameInput;
    }

    public void JoinLobbyOnClick() //Paired to delay start button
    {
        mainPanel.SetActive(false);
        lobbyPanel.SetActive(true);
        PhotonNetwork.JoinLobby(); //First Tries to join an existing room
    }


    public override void OnRoomListUpdate(List<RoomInfo> roomList) // when in a lobby, this function returns updates to lobbies
    {
        int tempIndex;
        foreach (RoomInfo room in roomList) //loop through each room
        {
            if (roomListings != null) //try to find existing room
            {
                tempIndex = roomListings.FindIndex(ByName(room.Name));
            }
            else
            {
                tempIndex = -1;
            }

            if (tempIndex != -1) //remove listing as its been closed
            {
                roomListings.RemoveAt(tempIndex);
                Destroy(roomsContainer.GetChild(tempIndex).gameObject);
            }

            if (room.PlayerCount > 0)
            {
                roomListings.Add(room);
                ListRoom(room);
            }
        }
    }

    static System.Predicate<RoomInfo> ByName(string name)
    {
        return delegate (RoomInfo room)
        {
            return room.Name == name;
        };
    }

    //Displays new room listing for the current room
    void ListRoom(RoomInfo room)
    {
        if (room.IsOpen && room.IsVisible)
        {
            GameObject tempListing = Instantiate(roomListingPrefab, roomsContainer);
            RoomButton tempButton = tempListing.GetComponent<RoomButton>();
            tempButton.SetRoom(room.Name, room.MaxPlayers, room.PlayerCount);

        }
    }

    //function for changing room name
    public void OnRoomNameChanaged(string nameIn)
    {
        roomName = nameIn;
    }

    //function for changing room size ~~ doesn't need to exist if max player count is set by default
    public void OnRoomSizeChanged(string sizeIn)
    {
        roomSize = int.Parse(sizeIn);
    }

    //function paired to create room button
    public void CreateRoom()
    {
        Debug.Log("Creating room now");
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)roomSize };
        PhotonNetwork.CreateRoom(roomName, roomOps); //attempt to create new room on server
    }

    public override void OnCreateRoomFailed(short returnCode, string message) //if create room fails, error message logged
    {
        Debug.Log("Create Room unsuccessful. Room with same name may already exist. Error Code: " + returnCode);
    }

    //paired to cancel button. Sends user back to menu
    public void MatchmakingCancel()
    {
        mainPanel.SetActive(true);
        lobbyPanel.SetActive(false);
        PhotonNetwork.LeaveLobby(); 
    }
}


