using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [Header("Login UI")]
    public InputField playerNameInputField;
    public GameObject ui_LoginGameObject;

    [Header("UI Lobby")]
    public GameObject ui_LobbyGameObject;

    [Header("Connection Status UI")]
    public GameObject ui_ConnectionStatusUI;
    public Text ui_ConnectionStatusText;
    public bool showConnectionStatus = false;
    



    #region Unity Methods

    // Start is called before the first frame update
    void Start()
    {
        ui_LoginGameObject.SetActive(true);

        ui_LobbyGameObject.SetActive(false);
        ui_ConnectionStatusUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (showConnectionStatus)
        {
            ui_ConnectionStatusText.text = "Connection Status: " + PhotonNetwork.NetworkClientState;
        }
        
    }

    #endregion

    #region UICallBackMethods
    
    public void OnQuickMatchButtonClicked()
    {
        SceneLoader.Instance.LoadScene("Scene_GameTest");
    }


    public void OnEnterGameButtonClicked()
    {
        ui_ConnectionStatusUI.SetActive(true);
     
        ui_LobbyGameObject.SetActive(false);
        ui_LoginGameObject.SetActive(false);
        string playerName = playerNameInputField.text;
        showConnectionStatus = true;

        if (!string.IsNullOrEmpty(playerName))
        {
            if (!PhotonNetwork.IsConnected)
            {
                PhotonNetwork.LocalPlayer.NickName = playerName;
                PhotonNetwork.ConnectUsingSettings();
            }
        }
        else
        {
            Debug.Log("player name is invalid or empty");
        }
    }

    #endregion


    #region PhotonCallBackMethods
    public override void OnConnected()
    {
        Debug.Log("We are connected to internet");
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log(PhotonNetwork.LocalPlayer.NickName + " is connected to photon server");
        ui_LobbyGameObject.SetActive(true);

        ui_LoginGameObject.SetActive(false);
        ui_ConnectionStatusUI.SetActive(false);
    }

    #endregion



}
