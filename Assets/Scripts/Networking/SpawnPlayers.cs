using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{

    public GameObject playerPrefab; //RightFacingPlayerSprite
    public GameObject playerPrefabLeft; //LeftFacingPlayerSprite

    //Determine spawn locations in unity inspector
    public float spawn1x;
    public float spawn1y;
    public float spawn2x;
    public float spawn2y;

    private void Start()
    {

        if (PhotonNetwork.IsMasterClient) //if host, spawn on left
        {
            Vector2 pos1 = new Vector2(spawn1x, spawn1y);
            PhotonNetwork.Instantiate(playerPrefab.name, pos1, Quaternion.identity);
        }
        else //if not host, spawn on right
        {
            Vector2 pos2 = new Vector2(spawn2x, spawn2y);
            PhotonNetwork.Instantiate(playerPrefabLeft.name, pos2, Quaternion.identity);
        }

    }

}
