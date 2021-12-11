using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

/***
 * 
 * Documentation: https://www.youtube.com/redirect?event=video_description&redir_token=QUFFLUhqblJUZlM3bHgtT3BJWll5d1IxelVVeng1eDBlUXxBQ3Jtc0treFd0V05PTC15bmVtVjcxNzZSdmJiOTJlRXBPWUp3Z3hUYUtGT0xJNUJQN0ZVWTZ3VjFMN0pLZ0ZPc0ZLMGlFMEQtUEJsWHFBWkl5SG1PLXhzSEQxTDM5cm82dHBGeTJoVF85eDdvTERxMlQwQkpuaw&q=https%3A%2F%2Fdoc.photonengine.com%2Fen-us%2Fpun%2Fcurrent%2Fgetting-started%2Fpun-intro
 * Scripting API: https://www.youtube.com/redirect?event=video_description&redir_token=QUFFLUhqbW9fZzBfc0ltWDM3RHF2WFNQUEpEVDBaSlJvQXxBQ3Jtc0tseTk3RTRMWHlydnQzYk41S3ZtX0JXYnBHWnl0b3QtV2RHb082V1dma2pVZXJyREw5amNhaDJGMGp2anVGcmlwcDRTalQ3MmcxUTB3NTBtTGJQWEFWTDdRQVQyOVRLd1hXOGJwWTNHTUlwUzVnWTJhZw&q=https%3A%2F%2Fdoc-api.photonengine.com%2Fen%2Fpun%2Fv2%2Findex.html
 * 
 */

public class NetworkController : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        //Connects to photon master server
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("We are now connected to the " + PhotonNetwork.CloudRegion + " server.");
    }
}
