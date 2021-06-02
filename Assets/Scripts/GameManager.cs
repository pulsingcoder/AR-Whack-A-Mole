using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System;
using Photon.Realtime;
using TMPro;


public class GameManager : MonoBehaviourPunCallbacks
{

    [Header("UI")]
    public GameObject ui_InformPanelGameObject;
    public TextMeshProUGUI ui_InformText;
    public GameObject ui_LeaderBoardGameObject;
    public TextMeshProUGUI ui_LeaderBoardText;
    public GameObject ui_JoinRandomRoomButton;
    public Text ui_GameCounter;
    private float timeCounter = 3f;
    private bool startGameCounter = false;
    private float gameTimer = 60f;
    public bool startGame = false;
    public Text ui_TimeText;
    public bool gameOver = false;
    public GameObject whack;
    public GameObject[] ui_PlayerList;
    public TextMeshProUGUI[] ui_TextPlayersName;
    private GameObject myself;
    public Button[] readyButtons;
    private int myindex;
    int countfalse = 0;
    public GameObject playerListPanel;
  
    
    
    // Start is called before the first frame update
    void Start()
    {
 //       players = new List<GameObject>();
        ui_InformPanelGameObject.SetActive(true);
        ui_JoinRandomRoomButton.SetActive(true);
        playerListPanel.SetActive(true);
        ui_InformText.text = "Search for Games";
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

       
        if (startGame)
        {
            if (Mathf.Floor(gameTimer) >=0f)
            {
                gameTimer -= Time.deltaTime;
                ui_TimeText.text = Mathf.Floor(gameTimer).ToString();
            }
            else
            {
                gameTimer = 60f;
                ui_TimeText.text = "0";
                startGame = false;
                gameOver = true;
                StartCoroutine(ShowLeaderBoard());
            }
        }
        if (startGameCounter)
        {
            
            if (Mathf.Floor(timeCounter) >=0f)
            {
                timeCounter -= Time.deltaTime;
                ui_GameCounter.text = Mathf.Floor(timeCounter).ToString();
            }
            else
            {
                timeCounter = 3f;
                ui_GameCounter.text = " ";
                startGameCounter = false;
                startGame = true;
                if (GameObject.FindGameObjectWithTag("Player").GetComponent<PhotonView>().IsMine)
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<ARShoot>().enabled = true;
                }
                playerListPanel.SetActive(false);
            }
        }

        if (!startGameCounter && !startGame)
        {
            countfalse = 0;
         
            var inGamePlayers = GameObject.FindGameObjectsWithTag("Player");
            print(inGamePlayers.Length);
            for (int i = 0; i < inGamePlayers.Length; i++)
            {
                print(inGamePlayers[i].GetComponent<PlayerSetup>().ready);
                if (!inGamePlayers[i].GetComponent<PlayerSetup>().ready)
                {
                   
                    countfalse++;
                }
            }
            if (countfalse == 0 && inGamePlayers.Length > 0)
            {
                print("true");
                startGameCounter = true;
                
            }
        }

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
       

        photonView.RPC("SetLobbyPlayerData", RpcTarget.AllBuffered);
        StartCoroutine(DeactivateAfterSeconds(ui_InformPanelGameObject, 2.0f));
        ui_JoinRandomRoomButton.SetActive(false);
        playerListPanel.SetActive(true);
        //if (PhotonNetwork.CurrentRoom.PlayerCount < 2)
        //{

        //    ui_InformText.text = "Jointed to room " + PhotonNetwork.CurrentRoom.Name + " Waiting for other player...";
        //}
        //else 
        //{
        //    ui_InformText.text = "Jointed to room " + PhotonNetwork.CurrentRoom.Name;
        //    StartCoroutine(DeactivateAfterSeconds(ui_InformPanelGameObject, 2.0f));

        //}
        //Debug.Log(PhotonNetwork.NickName + " Joined to room " + PhotonNetwork.CurrentRoom.Name);
        var localPlayers = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < localPlayers.Length; i++)
        {
            readyButtons[i].enabled = false;
            
                if (localPlayers[i].GetComponent<PhotonView>().IsMine)
            {
                print("Player is there");
                myself = localPlayers[i];
                myindex = PhotonNetwork.CurrentRoom.PlayerCount - 1;
                readyButtons[PhotonNetwork.CurrentRoom.PlayerCount-1].enabled = true;
                
                readyButtons[PhotonNetwork.CurrentRoom.PlayerCount - 1].onClick.AddListener(ReadyPlayers);
                //  localPlayers[i].GetComponent<PhotonView>().RPC("SetLobbyPlayerData", RpcTarget.AllBuffered);
            }
            

        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        
        //ui_InformText.text = newPlayer.NickName + " Joined the room" + " Player Count " + PhotonNetwork.CurrentRoom.PlayerCount;
        //if (PhotonNetwork.CurrentRoom.PlayerCount == 3)
        //{
        //    StartCoroutine(DeactivateAfterSeconds(ui_InformPanelGameObject, 2.0f));
        //}
       
    }

    #endregion
    [PunRPC] 
    public void SetLobbyPlayerData()
    {
        int i = 0;
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            ui_TextPlayersName[i].text = player.NickName;
            ui_PlayerList[i].SetActive(true);
            i++; 
        }     
    }

    [PunRPC] 
    public void SetReadyState(int index, string state)
    {
        print(state);
        readyButtons[index].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = state;
    }
    #region Private Methods 
    void ReadyPlayers()
    {
        print("You have Clicked");
        var state = "READY";
        if (myself.GetComponent<PlayerSetup>().ready)
        {
            myself.GetComponent<PlayerSetup>().ready = false;
            myself.GetComponent<PhotonView>().RPC("SetState", RpcTarget.AllBuffered, false);
            state = "UNREADY";
        }
        else
        {
            myself.GetComponent<PlayerSetup>().ready = true;
            myself.GetComponent<PhotonView>().RPC("SetState", RpcTarget.AllBuffered, true);
            state = "READY";
        }

        photonView.RPC("SetReadyState", RpcTarget.AllBuffered, myindex, state);

    }
    void CreateAndJoinRoom()
    {
        string roomName = "Room No: " + UnityEngine.Random.Range(0, 999);
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;

        PhotonNetwork.CreateRoom(roomName, roomOptions);
    }


    IEnumerator DeactivateAfterSeconds(GameObject _gameObject, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        _gameObject.SetActive(false);
      

    }

    IEnumerator ShowLeaderBoard()
    {
        yield return new WaitForSeconds(1f);
        print("Alola");
        var players = GameObject.FindGameObjectsWithTag("Player");
        for (int i=0;i<players.Length;i++)
        {
            print(players[i].GetComponent<PhotonView>().Owner.NickName + "Scrore " + players[i].GetComponent<ARShoot>().score);
        }
        GameObject[] sortedPlayers = players; 
        for (int i=0; i<players.Length;i++)
        {
            for (int j=1+1;j<players.Length-1;j++)
            {
                if (sortedPlayers[i].GetComponent<ARShoot>().score < sortedPlayers[j].GetComponent<ARShoot>().score)
                {

                    sortedPlayers[i] = sortedPlayers[j];
                    sortedPlayers[j] = players[i];
                }
            }
        }
        ui_LeaderBoardGameObject.SetActive(true);
        whack.SetActive(false);
        String boardText = "";
        print("YUvraj");
        for (int i=0;i<players.Length;i++)
        {
            boardText += sortedPlayers[i].GetComponent<PhotonView>().Owner.NickName + " " + "Score " +
                 sortedPlayers[i].GetComponent<ARShoot>().score + "\n";
        }
        ui_LeaderBoardText.text = boardText;
    }


    void SetPlayerOnList(int index, string playerName)
    {

        ui_PlayerList[index].SetActive(true);
        ui_TextPlayersName[index].text = playerName;

    }
    #endregion

}
