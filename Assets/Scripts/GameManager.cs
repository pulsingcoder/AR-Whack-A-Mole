using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;


public class GameManager : MonoBehaviourPunCallbacks
{

    [Header("UI")]
    public GameObject ui_InformPanelGameObject;
    public TextMeshProUGUI ui_InformText;
    // Start is called before the first frame update
    void Start()
    {
        ui_InformPanelGameObject.SetActive(true);
        ui_InformText.text = "Search for Games";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region UI_Callback Methods

    public void JoinRandomRoom()
    {
        ui_InformText.text = "Searching for available rooms...";

        PhotonNetwork.JoinRandomRoom();
    }

    #endregion



    #region Photon Callback Methods

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        CreateAndJoinRoom();
        ui_InformText.text = message;
    }

    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            ui_InformText.text = "Jointed to room " + PhotonNetwork.CurrentRoom.Name + " Waiting for other player...";
        }
        else
        {
            ui_InformText.text = "Jointed to room " + PhotonNetwork.CurrentRoom.Name;
            StartCoroutine(DeactivateAfterSeconds(ui_InformPanelGameObject, 2.0f));
        }
        Debug.Log(PhotonNetwork.NickName + " Joined to room " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        ui_InformText.text = newPlayer.NickName + " Joined the room" + " Player Count " + PhotonNetwork.CurrentRoom.PlayerCount;
        StartCoroutine(DeactivateAfterSeconds(ui_InformPanelGameObject, 2.0f));
        
    }

    #endregion



    #region Private Methods 

    void CreateAndJoinRoom()
    {
        string roomName = "Room No: " + Random.Range(0, 999);
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;

        PhotonNetwork.CreateRoom(roomName, roomOptions);
    }


    IEnumerator DeactivateAfterSeconds(GameObject _gameObject, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        _gameObject.SetActive(false);
    }

    #endregion

}
